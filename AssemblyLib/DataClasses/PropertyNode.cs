using AssemblyLib.Reflection;
using System.Reflection;

namespace AssemblyLib
{
    public class PropertyNode : INode
    {
        private string modifiers;

        public string Type { get; }
        public string Name { get; }

        internal PropertyNode(PropertyInfo prop)
        {
            Type = GetInfo.GetTypeName(prop.PropertyType);
            Name = prop.Name;
            modifiers = "public ";
        }

        public override string ToString()
        {
            return modifiers + Type + " " + Name;
        }
    }
}
