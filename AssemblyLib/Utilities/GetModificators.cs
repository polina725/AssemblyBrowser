using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyLib.Reflection
{
    public static class GetModificators
    {
        public enum DataType
        {
            Interface,
            Class,
            Struct,
            Enum,
            Delegate,
            Unknown
        }

        public enum AccessModificator
        {
            Public,
            Private,
            Internal,
            Protected,
            ProtectedPrivate,
            ProtectedInternal,
            Unknown
        }

        public enum TypeAttribute
        {
            Sealed,
            Static,
            Virtual,
            Abstract,
            Unknown
        }
        
        public enum PropertySetGet
        {
            Private,
            Public,
            NotDeclared
        }

        private static Dictionary<AccessModificator, string> accessModificator = new Dictionary<AccessModificator, string>
        {
            {AccessModificator.Public,  "public"},
            {AccessModificator.Private,  "private"},
            {AccessModificator.Protected,  "protected"},
            {AccessModificator.Internal,  "internal"},
            {AccessModificator.ProtectedInternal,  "protected internal"},
            {AccessModificator.ProtectedPrivate,  "protected private"},
            {AccessModificator.Unknown,  ""},//not defined modifier
        };

        private static Dictionary<TypeAttribute, string> typeAttribute = new Dictionary<TypeAttribute, string>
        {
            {TypeAttribute.Sealed,  "sealed"},
            {TypeAttribute.Abstract,  "abstract"},
            {TypeAttribute.Static,  "static"},
            {TypeAttribute.Virtual,  "virtual"},
            {TypeAttribute.Unknown,  ""},
        };

        private static Dictionary<DataType, string> dataType = new Dictionary<DataType, string>
        {
            {DataType.Interface,  "interface"},
            {DataType.Enum,  "enum"},
            {DataType.Struct,  "struct"},
            {DataType.Delegate,  "delegate"},
            {DataType.Class,  "class"},
            {DataType.Unknown,  ""},
        };

        public static string GetStringName(DataType dt)
        {
            string result;
            if (dataType.TryGetValue(dt, out result))
            {
                return result;
            }
            return null;
        }

        public static string GetStringName(AccessModificator am)
        {
            string result;
            if (accessModificator.TryGetValue(am, out result))
            {
                return result;
            }
            return null;
        }

        public static string GetStringName(TypeAttribute ta)
        {
            string result;
            if (typeAttribute.TryGetValue(ta, out result))
            {
                return result;
            }
            return null;
        }

        public static TypeAttribute GetTypeAtributes(Type typeInfo)
        {
            return typeInfo.IsAbstract && typeInfo.IsSealed ?
                TypeAttribute.Static :
            typeInfo.IsAbstract ?
                TypeAttribute.Abstract :
            typeInfo.IsSealed ?
                TypeAttribute.Sealed :
                TypeAttribute.Unknown;
        }

        public static TypeAttribute GetAttributes(MethodInfo methodInfo)
        {
            return methodInfo.IsVirtual ?
                TypeAttribute.Virtual :
            methodInfo.IsAbstract ?
                TypeAttribute.Abstract :
            methodInfo.IsStatic ?
                TypeAttribute.Static :
                TypeAttribute.Unknown;
        }

        public static DataType GetTypeClass(Type typeInfo)
        {
            return typeInfo.IsInterface ?
                DataType.Interface :
            typeInfo.IsEnum ?
                DataType.Enum :
            typeInfo.IsValueType ?
                DataType.Struct :
            (typeInfo.BaseType == typeof(MulticastDelegate)) ?
                DataType.Delegate :
            typeInfo.IsClass ?
                DataType.Class :
                DataType.Unknown;
        }

        public static AccessModificator GetAccessModificators(Type typeInfo)
        {
            return typeInfo.IsNestedPrivate ?
                AccessModificator.Private :
            typeInfo.IsNestedFamily ?
                AccessModificator.Protected :
            typeInfo.IsNestedAssembly ?
                AccessModificator.Internal :
            typeInfo.IsNestedFamORAssem ?
                AccessModificator.ProtectedInternal :
            typeInfo.IsNestedFamANDAssem ?
                AccessModificator.ProtectedPrivate :
            typeInfo.IsNestedPublic || typeInfo.IsPublic ?
                AccessModificator.Public :
            typeInfo.IsNotPublic ?
                AccessModificator.Private :
                AccessModificator.Public;
        }

        public static AccessModificator GetAccessModificators(MethodInfo memberInfo)
        {
            return memberInfo.IsPrivate ? 
                AccessModificator.Private :
            memberInfo.IsFamily ? 
                AccessModificator.Protected :
            memberInfo.IsAssembly ? 
                AccessModificator.Internal :
            memberInfo.IsFamilyOrAssembly ? 
                AccessModificator.ProtectedInternal :
            memberInfo.IsFamilyAndAssembly ? 
                AccessModificator.ProtectedPrivate :
            memberInfo.IsPublic ? 
                AccessModificator.Public : 
                AccessModificator.Unknown;
        }

        public static AccessModificator GetAccessModificators(FieldInfo fieldInfo)
        {
            return fieldInfo.IsPrivate ?
                AccessModificator.Private :
            fieldInfo.IsFamily ?
                AccessModificator.Protected :
            fieldInfo.IsAssembly ?
                AccessModificator.Internal :
            fieldInfo.IsFamilyOrAssembly ?
                AccessModificator.ProtectedInternal :
            fieldInfo.IsFamilyAndAssembly ?
                AccessModificator.ProtectedPrivate :
            fieldInfo.IsPublic ?
                AccessModificator.Public :
                AccessModificator.Unknown;
        }

        public static PropertySetGet GetGetModificator(PropertyInfo propertyInfo)
        {
            return propertyInfo.CanRead ? propertyInfo.GetGetMethod(false) != null ?
                PropertySetGet.Public :
                PropertySetGet.Private :
                PropertySetGet.NotDeclared;
        }

        public static PropertySetGet GetSetModificator(PropertyInfo propertyInfo)
        {
            return propertyInfo.CanWrite ? propertyInfo.GetSetMethod(false) != null ?
                PropertySetGet.Public :
                PropertySetGet.Private :
                PropertySetGet.NotDeclared;
        }

        public static string GetGetString(PropertySetGet propertyType)
        {
            switch (propertyType)
            {
                case PropertySetGet.NotDeclared: return "";
                case PropertySetGet.Public: return "public get";
                case PropertySetGet.Private: return "private get";
            }
            return null;
        }

        public static string GetSetString(PropertySetGet propertyType)
        {
            switch (propertyType)
            {
                case PropertySetGet.NotDeclared: return "";
                case PropertySetGet.Public: return "public set";
                case PropertySetGet.Private: return "private set";
            }
            return null;
        }
    }
}
