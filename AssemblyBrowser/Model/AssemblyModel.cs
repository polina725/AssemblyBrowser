using AssemblyLib;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Model
{
    class AssemblyModel : INotifyPropertyChanged
    {
        public AssemblyNode Assembly { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public AssemblyModel(string pathToAssembly)
        {
            Assembly = new AssemblyNode(pathToAssembly);
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
