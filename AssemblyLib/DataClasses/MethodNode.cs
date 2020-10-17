using AssemblyLib.Reflection;
using System.Reflection;

namespace AssemblyLib
{
    class MethodNode : INode
    {
        internal ParameterInfo[] Parameters { get; }
        private string modifiers;

        public string Name { get; }
        public string ReturnType { get; }

        internal MethodNode(MethodInfo method)
        {
            Name = method.Name;
            ReturnType = GetInfo.GetTypeName(method.ReturnType);
            if (method.IsPublic)
            {
                modifiers += "public ";
                if (method.IsVirtual)
                    modifiers += "virtual ";
            }
            else
                modifiers += "private ";
            if (method.IsStatic)
                modifiers += "static ";           
        }

        public override string ToString()
        {
            return GetInfo.GetSignature(this);
        }

    }
}
