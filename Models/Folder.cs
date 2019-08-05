using System.Collections.ObjectModel;


namespace FS_Explorer.Models
{
    public class Folder : Element
    {
        public ObservableCollection<Element> Children { get; set; } = new ObservableCollection<Element>();
        public string CountOfFolders { get; set; }
        public string CountOfFiles { get; set; }
        public string CreationTime { get; set; }
        public bool Selected { get; set; }
        bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.RaisePropertyChanged("IsExpanded");
                }

            }
        }


    }
}
