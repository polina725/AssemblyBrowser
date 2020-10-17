using AssemblyLib.Reflection;
using System.Reflection;

namespace AssemblyLib
{
    public class PropertyNode : INode
    {
        public string Type { get; }
        public string Name { get; }

        internal PropertyNode(PropertyInfo prop)
        {
            Type = GetInfo.GetTypeName(prop.PropertyType);
            Name = prop.Name;
        }

        public override string ToString()
        {
            return Type + " " + Name;
        }
    }
}
