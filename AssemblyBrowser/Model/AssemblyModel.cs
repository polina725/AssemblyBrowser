using AssemblyLib;

namespace AssemblyBrowser.Model
{
    class AssemblyModel
    {
        public AssemblyNode Assymbly { get; }

        public AssemblyModel(string pathToAssembly)
        {
            Assymbly = new AssemblyNode(pathToAssembly);
        }
    }
}
