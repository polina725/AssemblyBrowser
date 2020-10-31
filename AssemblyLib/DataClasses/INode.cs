using AssemblyLib.DataClasses;

namespace AssemblyLib
{
    public interface INode
    {
        public string Name { get; }
        public string Type { get; }
        public ModificatorsInfo Modificators{ get; }
        public string GetFullName();
    }
}
