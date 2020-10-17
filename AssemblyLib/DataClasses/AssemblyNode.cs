using AssemblyLib.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyLib
{
    public class AssemblyNode
    {
        public string Path { get; }
        public List<INode> Namespaces { get; }

        public AssemblyNode(string pathToAssembly)
        {
            try
            {
                Assembly ass = Assembly.LoadFrom(pathToAssembly);
                Path = pathToAssembly;
                Namespaces = new List<INode>();
                Dictionary<string, List<Type>> namespaceAndItsClasses = BindClassesWithNamespaces(ass.GetTypes());
                foreach (KeyValuePair<string, List<Type>> pair in namespaceAndItsClasses)
                {
                    Namespaces.Add(new NamespaceNode(pair.Key, pair.Value));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private Dictionary<string, List<Type>> BindClassesWithNamespaces(Type[] types)
        {
            Dictionary<string, List<Type>> namespaceAndItsClasses = new Dictionary<string, List<Type>>();
            foreach (Type t in types)
            {
                if (!namespaceAndItsClasses.ContainsKey(t.Namespace))
                    namespaceAndItsClasses.Add(t.Namespace, new List<Type>());
                namespaceAndItsClasses.TryGetValue(t.Namespace, out List<Type> classes);
                classes.Add(t);
            }
            return namespaceAndItsClasses;
        }
    }
}
