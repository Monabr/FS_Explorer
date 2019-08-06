using Humanizer.Bytes;
using Microsoft.WindowsAPICodePack.Shell;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Models;

namespace Models
{
    public class MainModel : BindableBase
    {
        private ObservableCollection<string> Drivers { get; }
        private ObservableCollection<Element> TreeOfFolders { get; set; } = new ObservableCollection<Element>();

        public readonly ReadOnlyObservableCollection<string> DriversPublicValue;
        public readonly ReadOnlyObservableCollection<Element> TreeOfFoldersPublicValue;

        public MainModel()
        {
            Drivers = new ObservableCollection<string>(DriveInfo.GetDrives().Select(y => y.Name).ToArray());
            DriversPublicValue = new ReadOnlyObservableCollection<string>(Drivers);
            TreeOfFoldersPublicValue = new ReadOnlyObservableCollection<Element>(TreeOfFolders);
        }


        public void DriversSelectionChanged(object x)
        {
            TreeOfFolders.Clear();
            try
            {
                foreach (DirectoryInfo dir in new DirectoryInfo(x.ToString()).GetDirectories().OrderBy(y => y.Name).ToArray())
                {

                    ShellObject shellFolder = ShellObject.FromParsingName(dir.FullName.ToString());
                    BitmapSource shellThumb = shellFolder.Thumbnail.SmallBitmapSource;

                    Folder folder = new Folder
                    {
                        Name = dir.ToString(),
                        Icon = shellThumb,
                        FullName = dir.FullName,
                    };
                    try
                    {
                        folder.CountOfFolders = new DirectoryInfo(dir.FullName).GetDirectories().Length.ToString();
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        folder.CountOfFolders = "Отказано в доступе";
                    }

                    try
                    {
                        folder.CountOfFiles = new DirectoryInfo(dir.FullName).GetFiles().Length.ToString();
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        folder.CountOfFiles = "Отказано в доступе";
                    }

                    try
                    {
                        folder.CreationTime = dir.CreationTime.ToString(CultureInfo.InvariantCulture);
                    }
                    catch (UnauthorizedAccessException ex)
                    {

                    }

                    folder.Children.Add(new Folder
                    {
                        Name = "*",
                        Icon = null,
                        FullName = dir.FullName
                    });

                    TreeOfFolders.Add(folder);

                }


                foreach (FileInfo file in new DirectoryInfo(x.ToString()).GetFiles().OrderBy(y => y.Name).ToArray())
                {

                    Icon icon =
                        (System.Drawing.Icon)System.Drawing.Icon.ExtractAssociatedIcon(file.FullName.ToString());


                    if (file.Extension.ToLower() == ".jpg" || file.Extension.ToLower() == ".jpeg" || file.Extension.ToLower() == ".bmp" || file.Extension.ToLower() == ".png")
                    {
                        TreeOfFolders.Add(
                            new CustomImage
                            {
                                Name = file.ToString(),
                                Icon = icon.ToImageSource(),
                                FullName = file.FullName.ToString(),
                                Size = ByteSize.FromBytes(file.Length).ToString(),
                                CreationTime = file.CreationTime.ToString(CultureInfo.InvariantCulture)
                            }
                        );
                    }
                    else
                    {
                        TreeOfFolders.Add(
                            new CustomFile
                            {
                                Name = file.ToString(),
                                Icon = icon.ToImageSource(),
                                FullName = file.FullName.ToString(),
                                Size = ByteSize.FromBytes(file.Length).ToString(),
                                CreationTime = file.CreationTime.ToString(CultureInfo.InvariantCulture)
                            }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@ex.Message);
            }

        }

        public void Expanded(RoutedEventArgs arg)
        {
            TreeViewItem item = (TreeViewItem)arg.OriginalSource;
            Element fileInfo = (Element)item.DataContext;
            Folder folder = (Folder)fileInfo;
            folder.GetChildren();
        }

        public void ExpandChildrenSelected(Folder arg)
        {
            arg.IsExpanded = true;
            arg.GetChildren();
            arg.ExpandChildren();
        }



    }
}





