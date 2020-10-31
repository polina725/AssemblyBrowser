using AssemblyLib.DataClasses;
using AssemblyLib.Reflection;

using System.Reflection;

namespace AssemblyLib
{
    public class FieldNode : INode
    {
        public string Type { get; }
        public string Name { get; }
        public ModificatorsInfo Modificators { get; }

        internal FieldNode(FieldInfo field)
        {
            Type = GetNames.GetTypeName(field.FieldType);
            Name = field.Name;
            Modificators = new ModificatorsInfo(field);
        }

        public string GetFullName()
        {
            return Modificators.AccessModificatorString + " " + Modificators.TypeAttributeString + " " + Type + " " + Name;
        }
    }
}
