using System;
using System.Collections.Generic;

using AssemblyLib.Reflection;

namespace AssemblyLib
{
    class ClassNode : INode
    {
        private string modifiers;

        public string Name { get; }
        public List<INode> Fields { get; }
        public List<INode> Properties { get; }
        public List<INode> Methods { get; }

        internal ClassNode(Type t)
        {
            Name = GetInfo.GetTypeName(t);
            if (t.IsAbstract)
                modifiers += "abstract ";
            if (t.IsInterface)
                modifiers += "interface ";
            else if (t.IsClass)
                modifiers += "class ";
            Properties = GetInfo.GetListOfData(t.GetProperties());
            Fields = GetInfo.GetListOfData(t.GetFields());
            Methods = GetInfo.GetListOfData(t.GetMethods());
        }

        public override string ToString()
        {
            string tmp = "";
            tmp += (smth(Fields) + smth(Properties) + smth(Methods));
            return modifiers + " " + Name + "\n" + tmp + "\n";
        }

        private string smth(List<INode> l)
        {
            string tmp = "";
            foreach (INode d in l)
            {
                tmp += ("\t\t" + d + "\n");
            }
            return tmp;
        }
    }
}
