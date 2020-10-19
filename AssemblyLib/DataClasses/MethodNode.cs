using AssemblyLib.Reflection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyLib
{
    public class MethodNode : INode
    {
        internal ParameterInfo[] Parameters { get; }
        private string modifiers;
        private bool isExtension;

        public string Name { get; }
        public string ReturnType { get; }

        internal MethodNode(MethodInfo method)
        {
            Name = method.Name;
            ReturnType = GetInfo.GetTypeName(method.ReturnType);
            Parameters = method.GetParameters();
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
            isExtension = method.IsDefined(typeof(ExtensionAttribute), false);
        }

        private string GetSignature(MethodNode method)
        {
            string signature = "";
            signature += (method.ReturnType + " " + method.Name + "(");
            if (method.Parameters.Length == 0)
                return signature + ")";
            foreach (ParameterInfo p in method.Parameters)
            {
                if (p.IsOut)
                    signature += "out ";
                signature += (GetInfo.GetTypeName(p.ParameterType) + " " + p.Name + ", ");
            }
            while (signature.IndexOf('&') != -1)
            {
                signature.Replace('&', ' ');
            }
            return signature.Substring(0, signature.Length - 2) + ")";
        }

        public override string ToString()
        {
            return modifiers + GetSignature(this);
        }
    }
}
