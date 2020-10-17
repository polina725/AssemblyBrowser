using AssemblyLib.Reflection;
using System.Reflection;

namespace AssemblyLib
{
    class FieldNode : INode
    {
        public string Type { get; }
        public string Name { get; }

        internal FieldNode(FieldInfo field)
        {
            Type = GetInfo.GetTypeName(field.FieldType);
            Name = field.Name;
        }


        public override string ToString()
        {
            return Type + " " + Name;
        }
    }
}
