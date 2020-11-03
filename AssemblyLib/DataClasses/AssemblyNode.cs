using AssemblyLib.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyLib
{
    public class AssemblyNode
    {
        public string Path { get; }
        public List<INode> Namespaces { get; }

        public AssemblyNode(string pathToAssembly)
        {
            //try
            //{
            Path = pathToAssembly;
            Assembly ass;
            try
            {
                ass = Assembly.LoadFrom(pathToAssembly);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            Namespaces = new List<INode>();
            Type[] types;
            try
            {
                types = ass.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null).ToArray();
            }
            Dictionary<string, List<Type>> namespaceAndItsClasses = BindClassesWithNamespaces(types);
            foreach (KeyValuePair<string, List<Type>> pair in namespaceAndItsClasses)
            {
                Namespaces.Add(new NamespaceNode(pair.Key, pair.Value));
            }
        }

        private Dictionary<string, List<Type>> BindClassesWithNamespaces(Type[] types)
        {
            Dictionary<string, List<Type>> namespaceAndItsClasses = new Dictionary<string, List<Type>>();
            foreach (Type t in types)
            {
                string namespaceS;
                if (t.Namespace != null)
                    namespaceS = t.Namespace;
                else
                    namespaceS = "<>";
                if (!namespaceAndItsClasses.ContainsKey(namespaceS))
                {
                    List<Type> tmp = new List<Type>();
                    namespaceAndItsClasses.Add(namespaceS, tmp);
                }
                namespaceAndItsClasses.TryGetValue(namespaceS, out List<Type> classes);
                classes.Add(t);
            }
            return namespaceAndItsClasses;
        }
    }
}
