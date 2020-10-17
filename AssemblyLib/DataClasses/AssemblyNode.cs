using AssemblyLib.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyLib
{
    public class AssemblyNode
    {
        public string Path { get; }
        public List<NamespaceNode> Namespaces { get; }

        public AssemblyNode(string pathToAssembly)
        {
            Path = pathToAssembly;
            Assembly ass = Assembly.LoadFrom(pathToAssembly);
            Namespaces = new List<NamespaceNode>();
            Dictionary<string, List<Type>> namespaceAndItsClasses = GetInfo.BindClassesWithNamespaces(ass.GetTypes());
            foreach (KeyValuePair<string, List<Type>> pair in namespaceAndItsClasses)
            {
                Namespaces.Add(new NamespaceNode(pair.Key, pair.Value));
            }
        }

        public override string ToString()
        {
            string tmp = "";
            foreach(NamespaceNode nm in Namespaces)
            {
                tmp += (nm + "\n");
            }
            return Path+"\n"+tmp;
        }
    }
}
