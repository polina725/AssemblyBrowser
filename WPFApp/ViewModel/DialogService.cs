using Microsoft.Win32;

namespace AssemblyBrowser.ViewModel
{
    class DialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Assemblies (*.dll) | *.dll";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
    }
}
