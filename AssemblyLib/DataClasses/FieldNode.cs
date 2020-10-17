using AssemblyLib.Reflection;

using System.Reflection;

namespace AssemblyLib
{
    public class FieldNode : INode
    {
        private string modifiers = "";

        public string Type { get; }
        public string Name { get; }

        internal FieldNode(FieldInfo field)
        {
            Type = GetInfo.GetTypeName(field.FieldType);
            Name = field.Name;
            modifiers = field.IsPublic ? "public " : "private ";
            if (field.IsStatic)
                modifiers += "static ";
        }

        public override string ToString()
        {
            return Type + " " + Name;
        }
    }
}
