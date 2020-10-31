using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AssemblyLib;

namespace AssemblyBrowser.Model
{
    public class ClassNodeView : INotifyPropertyChanged
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

        private List<MemberView> members;
        public List<MemberView> Members
        {
            get { return members; }
            set
            {
                members = value;
                OnPropertyChanged("Memebers");
            }
        }

        public ClassNodeView(ClassNode classNode)
        {
            Name = classNode.GetFullName();
            List<MemberView> prop = classNode.Properties.ConvertAll(p => new MemberView((PropertyNode)p));
            List<MemberView> methods = classNode.Methods.ConvertAll(m => new MemberView((MethodNode)m));
            List<MemberView> fields = classNode.Fields.ConvertAll(f => new MemberView((FieldNode)f));
            fields.AddRange(prop);
            fields.AddRange(methods);
            Members = fields.ConvertAll(m => m);
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
