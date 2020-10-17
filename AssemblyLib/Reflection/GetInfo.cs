using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyLib.Reflection
{
    internal class GetInfo
    {
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
    }
}
