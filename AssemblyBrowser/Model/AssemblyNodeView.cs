using AssemblyLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Model
{
    public class AssemblyNodeView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private List<NamespaceNodeView> namespaces;
        private List<NamespaceNodeView> Namespaces
        {
            get { return namespaces; }
            set
            {
                namespaces = value;
                OnPropertyChanged("Namespaces");
            }
        }


        public AssemblyNodeView(AssemblyNode ass)
        {
            Namespaces = ass.Namespaces.ConvertAll(assemblyNamespace => new NamespaceNodeView((NamespaceNode)assemblyNamespace));
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
