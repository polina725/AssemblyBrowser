using AssemblyLib.DataClasses;
using System;
using System.Collections.Generic;
using static AssemblyLib.Utilities.CompilerAttr;

namespace AssemblyLib
{
    public class NamespaceNode : INode
    {
        public string Name { get; }
        public string Type { get { return ""; } }
        public ModificatorsInfo Modificators { get { return null; } }

        public List<INode> Classes { get; }

        internal NamespaceNode(string namespc, List<Type> types)
        {
            Name = namespc;
            Classes = new List<INode>();
            foreach(Type t in types)
            {
                if(!CompilerGenerated(t))
                    Classes.Add(new ClassNode(t));
            }
        }

        public string GetFullName()
        {
            return Name;
        }
    }
}
