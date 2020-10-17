namespace AssemblyBrowser.ViewModel
{
    public interface IDialogService
    {
        string FilePath { get; set; } 
        bool Open();  
    }
}
