using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyLib.Utilities
{
    public static class CompilerAttr
    {
        public static bool CompilerGenerated(MemberInfo member)
        {
            bool compilerGenerated = false;

            compilerGenerated |= (Attribute.GetCustomAttribute(member, typeof(CompilerGeneratedAttribute)) != null);

            if (member.MemberType == MemberTypes.Method)
            {
                compilerGenerated |= ((MethodInfo)member).IsSpecialName;
            }
            else if (member.MemberType == MemberTypes.Property )
            {
                compilerGenerated |= ((PropertyInfo)member).IsSpecialName;
            }

            return compilerGenerated;
        }
    }
}
