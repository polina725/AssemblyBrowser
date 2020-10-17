using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyLib;

namespace AssemblyBrowser.Model
{
    public class MemberView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public MemberView(FieldNode field)
        {
            FullName = field.ToString();
        }

        public MemberView(PropertyNode prop)
        {
            FullName = prop.ToString();
        }

        public MemberView(MethodNode method)
        {
            FullName = method.ToString();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
