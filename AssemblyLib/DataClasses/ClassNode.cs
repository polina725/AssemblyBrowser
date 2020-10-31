using System;
using System.Collections.Generic;
using System.Reflection;
using AssemblyLib.DataClasses;
using AssemblyLib.Reflection;
using static AssemblyLib.Utilities.CompilerAttr;

namespace AssemblyLib
{
    public class ClassNode : INode
    {
        public string Name { get; }
        public string Type { get { return ""; } }
        public ModificatorsInfo Modificators { get; }

        public List<INode> Fields { get; }
        public List<INode> Properties { get; }
        public List<INode> Methods { get; }

        internal ClassNode(Type t)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly; 
            Name = GetNames.GetTypeName(t);
            Modificators = new ModificatorsInfo(t);
            Properties = GetListOfData(t.GetProperties(flags));
            Fields = GetListOfData(t.GetFields(flags));
            Methods = GetListOfData(t.GetMethods(flags));
        }

        private List<INode> GetListOfData(MemberInfo[] members)
        {
            List<INode> list = new List<INode>();
            foreach (MemberInfo member in members)
            {
                if (CompilerGenerated(member))
                    continue;
                if (member.MemberType.Equals(MemberTypes.Field))
                    list.Add(new FieldNode((FieldInfo)member));
                else if (member.MemberType.Equals(MemberTypes.Property))
                    list.Add(new PropertyNode((PropertyInfo)member));
                else if (member.MemberType.Equals(MemberTypes.Method))
                    list.Add(new MethodNode((MethodInfo)member));
            }
            return list;
        }

        public string GetFullName()
        {
            return Modificators.AccessModificatorString + " " + Modificators.TypeAttributeString + " " + Modificators.DataTypeString + " " + Name;
        }
    }
}
