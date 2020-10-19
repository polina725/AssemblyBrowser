using AssemblyBrowser.Model;
using AssemblyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssemblyBrowser.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private string assemblyName = "mem";
        public string AssemblyName 
        { 
            get { return assemblyName; }
            set
            {
                assemblyName = value;
                OnPropertyChanged("AssemblyName");
            }
        }

        private List<NamespaceNodeView> namespaces;
        public List<NamespaceNodeView> Namespaces
        {
            get { return namespaces; }
            set
            {
                namespaces = value;
                OnPropertyChanged("Namespaces");
            }
        }

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
                          try
                          {
                              Namespaces = new AssemblyNode(dialogService.FilePath).Namespaces.ConvertAll(assemblyNamespace => new NamespaceNodeView((NamespaceNode)assemblyNamespace));
                          }catch(Exception ex)
                          {
                              MessageBox.Show(ex.Message);
                          }
                          AssemblyName = dialogService.FilePath;
                      }
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
