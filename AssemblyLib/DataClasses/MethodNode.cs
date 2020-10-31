using AssemblyLib.DataClasses;
using AssemblyLib.Reflection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyLib
{
    public class MethodNode : INode
    {
        internal ParameterInfo[] Parameters { get; }
        public string Name { get; }
        public string Type { get; }
        public ModificatorsInfo Modificators { get; }

        public string ReturnType { get; }

        internal MethodNode(MethodInfo method)
        {
            Name = method.Name;
            Type = GetNames.GetTypeName(method.ReturnType);
            Parameters = method.GetParameters();
            Modificators = new ModificatorsInfo(method);

        }

        private string GetSignature(MethodNode method)
        {
            string signature = "";
            signature += (method.Type + " " + method.Name + "(");
            if (method.Parameters.Length == 0)
                return signature + ")";
            foreach (ParameterInfo p in method.Parameters)
            {
                if (p.IsOut)
                    signature += "out ";
                signature += (GetNames.GetTypeName(p.ParameterType) + " " + p.Name + ", ");
            }
            while (signature.IndexOf('&') != -1)
            {
                signature = signature.Replace('&', ' ');
            }
            return signature.Substring(0, signature.Length - 2) + ")";
        }

        public string GetFullName()
        {
            return Modificators.AccessModificatorString + " " + Modificators.TypeAttributeString + " " + GetSignature(this); ;
        }
    }
}
