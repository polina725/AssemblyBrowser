using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AssemblyLib;

namespace AssemblyBrowser.Model
{
    public class NamespaceNodeView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private List<ClassNodeView> classes;
        public List<ClassNodeView> Classes
        {
            get { return classes; }
            set
            {
                classes = value;
                OnPropertyChanged("Classes");
            }
        }

        public NamespaceNodeView(NamespaceNode namespaceNode)
        {
            Name = namespaceNode.Name;
            Classes= namespaceNode.Classes.ConvertAll(cl => new ClassNodeView((ClassNode)cl));
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
