using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyLib.Reflection
{
    internal class GetInfo
    {
        internal static List<INode> GetListOfData(MemberInfo[] members)
        {
            List<INode> list = new List<INode>();
            foreach(MemberInfo member in members)
            {
                if (member.MemberType.Equals(MemberTypes.Field))
                    list.Add(new FieldNode((FieldInfo)member));
                else if (member.MemberType.Equals(MemberTypes.Property))
                    list.Add(new PropertyNode((PropertyInfo)member));
                else if(member.MemberType.Equals(MemberTypes.Method))
                    list.Add(new MethodNode((MethodInfo)member));
            }
            return list;
        }

        internal static Dictionary<string,List<Type>> BindClassesWithNamespaces(Type[] types)
        {
            Dictionary<string, List<Type>> namespaceAndItsClasses = new Dictionary<string, List<Type>>();
            foreach (Type t in types)
            {
                if (!namespaceAndItsClasses.ContainsKey(t.Namespace))
                    namespaceAndItsClasses.Add(t.Namespace, new List<Type>());
                namespaceAndItsClasses.TryGetValue(t.Namespace, out List<Type> classes);
                classes.Add(t);
            }
            return namespaceAndItsClasses;
        }

        internal static string GetTypeName(Type t)
        {
            if (t.IsGenericType)
                return GetGenericTypeName(t);
            return t.Name;
        }

        private static string GetGenericTypeName(Type t)
        {
            string typeName = "";
            string tmp = t.GetGenericTypeDefinition().Name;
            int ind = tmp.LastIndexOf('`');
            typeName += (tmp.Substring(0,ind) + "<");
            Type[] argTypes = t.GetGenericArguments();
            foreach (Type argType in argTypes)
            {
                if (argType.IsGenericType)
                    typeName += (GetTypeName(argType) + ", ");
                else
                    typeName += (argType.Name + ", ");

            }
            typeName = typeName.Substring(0,typeName.Length-2) + ">";
            return typeName;
        }

        internal static string GetSignature(MethodNode method)
        {
            string signature = "";           
            signature += (method.ReturnType + " " + method.Name + "(");
            if (method.Parameters == null)
                return signature + ")";
            foreach (ParameterInfo p in method.Parameters)
            {
                if (p.IsOut)
                    signature += "out ";
                signature += (GetTypeName(p.ParameterType) + " " + p.Name + ", ");
            }
            while (signature.IndexOf('&') != -1){
                signature.Replace('&', ' ');
            }
            return signature.Substring(0, signature.Length - 2) + ")";
        }
    }
}
