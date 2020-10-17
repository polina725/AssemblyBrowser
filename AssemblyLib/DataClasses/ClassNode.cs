using System;
using System.Collections.Generic;
using System.Reflection;
using AssemblyLib.Reflection;

namespace AssemblyLib
{
    public class ClassNode : INode
    {
        private string modifiers = "";

        public string Name { get; }
        public List<INode> Fields { get; }
        public List<INode> Properties { get; }
        public List<INode> Methods { get; }

        internal ClassNode(Type t)
        {
            Name = GetInfo.GetTypeName(t);
            modifiers = GetAttributes(t) + GetTypeClass(t);
            Properties = GetListOfData(t.GetProperties());
            Fields = GetListOfData(t.GetFields());
            Methods = GetListOfData(t.GetMethods());
        }

        private List<INode> GetListOfData(MemberInfo[] members)
        {
            List<INode> list = new List<INode>();
            foreach (MemberInfo member in members)
            {
                if (member.MemberType.Equals(MemberTypes.Field))
                    list.Add(new FieldNode((FieldInfo)member));
                else if (member.MemberType.Equals(MemberTypes.Property))
                    list.Add(new PropertyNode((PropertyInfo)member));
                else if (member.MemberType.Equals(MemberTypes.Method))
                    list.Add(new MethodNode((MethodInfo)member));
            }
            return list;
        }

        private string GetAttributes(Type t)
        {
            if (t.IsAbstract && t.IsSealed)
                return "static ";
            if (t.IsSealed)
                return "sealed ";
            if (t.IsAbstract)
                return "abstract ";
            return "";
        }

        private string GetTypeClass(Type t)
        {
            if (t.IsInterface)
                return "interface ";
            if (t.IsEnum)
                return "enum ";
            if (t.IsValueType)
                return "struct ";
            if (t.IsClass)
                return "class ";
            return "";
        }

        public override string ToString()
        {
            return modifiers + " " + Name;
        }
    }
}
