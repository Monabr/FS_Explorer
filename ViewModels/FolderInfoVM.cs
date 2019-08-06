using Models;

namespace FS_Explorer.ViewModels
{
    public class FolderInfoVM : ElementVM
    {
        private Folder _folder;
        public Folder Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
                this.RaisePropertyChanged("Folder");
            }
        }

        public FolderInfoVM()
        {

        }
        public FolderInfoVM(Folder folder)
        {
            Folder = folder;
        }
    }
}
