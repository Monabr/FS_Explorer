using Models;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

namespace FS_Explorer.ViewModels
{
    public class ViewModel : BindableBase
    {
        private ElementVM _selementElement;
        private static MainModel mainModel = new MainModel();
        public ObservableCollection<String> Drivers { get; set; }
        public ObservableCollection<Element> TreeOfFolders { get; set; }
        public ElementVM SelectedElement
        {
            get { return _selementElement; }
            set
            {
                _selementElement = value;
                this.RaisePropertyChanged("SelectedElement");
            }
        }


        public ViewModel()
        {
            Drivers = new ObservableCollection<string>(mainModel.DriversPublicValue);
            TreeOfFolders = new ObservableCollection<Element>(mainModel.TreeOfFoldersPublicValue);
            Watch(mainModel.TreeOfFoldersPublicValue, TreeOfFolders);
        }


        private static void Watch<T>(ReadOnlyObservableCollection<T> collToWatch, ObservableCollection<T> collToUpdate)
        {
            ((INotifyCollectionChanged)collToWatch).CollectionChanged += (s, a) =>
            {
                if (a.NewItems?.Count == 1) collToUpdate.Add((T)a.NewItems[0]);
                if (a.OldItems?.Count == null && a.NewItems?.Count == null) collToUpdate.Clear();

            };
        }


        private void LoadInfo(Element x)
        {
            if (x is Folder folder)
            {
                SelectedElement = new FolderInfoVM(folder);
            }
            else if (x is CustomImage customImage)
            {
                SelectedElement = new CustomImageInfoVM(customImage);
            }
            else if (x is CustomFile customFile)
            {
                SelectedElement = new CustomFileInfoVM(customFile);
            }

        }



        private ICommand _expandingCommand;
        public ICommand ExpandingCommand
        {
            get
            {
                return _expandingCommand ?? (_expandingCommand = new RelayCommand(x =>
                {
                    mainModel.Expanded((RoutedEventArgs)x);
                }, (x) =>
                {
                    RoutedEventArgs y = (RoutedEventArgs)x;
                    return y.OriginalSource.ToString().Contains("Folder Items.Count:1");
                }));
            }
        }

        private ICommand _selectionChanged;
        public ICommand SelectionChanged
        {
            get
            {
                return _selectionChanged ?? (_selectionChanged = new RelayCommand(x =>
                {
                    mainModel.DriversSelectionChanged(x);
                }));
            }
        }

        private ICommand _itemSelected;
        public ICommand ItemSelected
        {
            get
            {
                return _itemSelected ?? (_itemSelected = new RelayCommand(x =>
                {
                    LoadInfo((Element)x);
                }));
            }
        }

        private ICommand _folderContextFirst;
        public ICommand FolderContextFirst
        {
            get
            {
                return _folderContextFirst ?? (_folderContextFirst = new RelayCommand(x =>
                {
                    mainModel.ExpandChildrenSelected((Folder)x);
                }));
            }
        }

        private ICommand _folderContextSecond;
        public ICommand FolderContextSecond
        {
            get
            {
                return _folderContextSecond ?? (_folderContextSecond = new RelayCommand(x =>
                {
                    mainModel.OpenSelected((Folder)x);
                }));
            }
        }

        private ICommand _fileContext;
        public ICommand FileContext
        {
            get
            {
                return _fileContext ?? (_fileContext = new RelayCommand(x =>
                {
                    mainModel.OpenSelected((CustomFile)x);
                }));
            }
        }
    }
}
