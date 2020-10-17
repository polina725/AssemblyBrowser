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

        //private List<INode> fields;
        //public List<INode> Fields 
        //{
        //    get { return fields; } 
        //    set
        //    {
        //        fields = value;
        //        OnPropertyChanged("Fields");
        //    }
        //}

        //private List<INode> properties;
        //public List<INode> Properties 
        //{
        //    get { return properties; }
        //    set
        //    {
        //        properties = value;
        //        OnPropertyChanged("Properties");
        //    }
        //}

        //private List<INode> methods;
        //public List<INode> Methods 
        //{
        //    get { return methods; }
        //    set
        //    {
        //        methods = value;
        //        OnPropertyChanged("Methods");
        //    }
        //}

        public ClassNodeView(ClassNode classNode)
        {
            Name = classNode.ToString();
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
