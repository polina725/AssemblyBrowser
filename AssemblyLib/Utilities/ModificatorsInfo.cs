using System;
using System.Reflection;
using static AssemblyLib.Reflection.GetModificators;

namespace AssemblyLib.DataClasses
{
    public class ModificatorsInfo
    {
        public DataType DataType { get; } = DataType.Unknown;
        public string DataTypeString 
        { 
            get 
            { 
                return GetStringName(DataType); 
            } 
        }

        public AccessModificator Access { get; } = AccessModificator.Unknown;
        public string AccessModificatorString 
        {
            get
            {
                return GetStringName(Access);
            }
        }

        public TypeAttribute TypeAttribute { get; } = TypeAttribute.Unknown;
        public string TypeAttributeString
        {
            get
            {
                return GetStringName(TypeAttribute);
            }
        }

        public ModificatorsInfo(FieldInfo field)
        {
            DataType = DataType.Unknown;
            Access = GetAccessModificators(field);
            if (field.IsStatic)
                TypeAttribute = TypeAttribute.Static;
            else
                TypeAttribute = TypeAttribute.Unknown;
        }

        public ModificatorsInfo(Type t)
        {
            DataType = GetTypeClass(t);
            Access = GetAccessModificators(t);
            TypeAttribute = GetTypeAtributes(t);

        }

        public ModificatorsInfo(MethodInfo method)
        {
            DataType = DataType.Unknown;
            Access = GetAccessModificators(method);
            TypeAttribute = GetAttributes(method);
        }

        public ModificatorsInfo() { }

    }
}
