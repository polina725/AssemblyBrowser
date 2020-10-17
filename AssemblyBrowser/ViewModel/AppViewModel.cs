using AssemblyBrowser.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private AssemblyModel ass;

        private MyCommand openFile;
        public MyCommand OpenFile
        {
            get
            {
                return openFile ??
                  (openFile = new MyCommand(obj =>
                  {
                      IDialogService dialogService = new DialogService();
                      if (dialogService.Open())
                      {
                          ass = new AssemblyModel(dialogService.FilePath);
                      }
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
