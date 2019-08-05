using FS_Explorer.Models;

namespace FS_Explorer.ViewModels
{
    class CustomFileInfoVM : ElementVM
    {
        private CustomFile _file;
        public CustomFile File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
                this.RaisePropertyChanged("File");
            }
        }

        public CustomFileInfoVM()
        {

        }
        public CustomFileInfoVM(CustomFile file)
        {
            File = file;
        }
    }
}
