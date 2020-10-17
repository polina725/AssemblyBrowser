using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLib
{
    public class NamespaceNode : INode
    {
        public string Name { get; }
        public List<INode> Classes { get; }

        internal NamespaceNode(string namespc, List<Type> types)
        {
            Name = namespc;
            Classes = new List<INode>();
            foreach(Type t in types)
            {
                Classes.Add(new ClassNode(t));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
