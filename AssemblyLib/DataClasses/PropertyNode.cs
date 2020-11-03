using AssemblyLib.DataClasses;
using AssemblyLib.Reflection;
using System.Reflection;
using static AssemblyLib.Reflection.GetModificators;

namespace AssemblyLib
{
    public class PropertyNode : INode
    {
        public string Type { get; }
        public string Name { get; }
        public ModificatorsInfo Modificators { get; }
        public PropertySetGet GetModificator { get; }
        public string GetModificatorString { get { return GetGetString(GetModificator); } }
        public PropertySetGet SetModificator { get; }
        public string SetModificatorString { get { return GetSetString(SetModificator); } }

        internal PropertyNode(PropertyInfo prop)
        {
            Type = GetNames.GetTypeName(prop.PropertyType);
            Name = prop.Name;
            Modificators = new ModificatorsInfo();
            GetModificator = GetGetModificator(prop);
            SetModificator = GetSetModificator(prop);
        }

        public string GetFullName()
        {
            return Modificators.AccessModificatorString + " " + Modificators.TypeAttributeString + " " + Type + " " + Name + " { " + GetModificatorString + "; " + SetModificatorString + " }";
        }

    }
}
