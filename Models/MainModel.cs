using ByteSizeLib;
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

namespace FS_Explorer.Models
{
    class MainModel : BindableBase
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


        public void GetTree(object x)
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

        public void GetMoreTree(RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            item.ClearValue(ItemsControl.ItemsSourceProperty);

            ObservableCollection<Element> observableCollection = new ObservableCollection<Element>();

            Element fileInfo = (Element)item.DataContext;



            try
            {


                foreach (DirectoryInfo dir in new DirectoryInfo(fileInfo.FullName).GetDirectories().OrderBy(x => x.Name).ToArray())
                {
                    ShellObject shellFolder = ShellObject.FromParsingName(dir.FullName.ToString());
                    BitmapSource shellThumb = shellFolder.Thumbnail.SmallBitmapSource;

                    Folder newItem = new Folder
                    {
                        Name = dir.ToString(),
                        Icon = shellThumb,
                        FullName = dir.FullName,
                    };


                    try
                    {
                        newItem.CountOfFolders = new DirectoryInfo(dir.FullName).GetDirectories().Length.ToString();
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        newItem.CountOfFolders = "Отказано в доступе";
                    }

                    try
                    {
                        newItem.CountOfFiles = new DirectoryInfo(dir.FullName).GetFiles().Length.ToString();
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        newItem.CountOfFiles = "Отказано в доступе";
                    }

                    try
                    {
                        newItem.CreationTime = dir.CreationTime.ToString(CultureInfo.InvariantCulture);
                    }
                    catch (UnauthorizedAccessException ex)
                    {

                    }


                    newItem.Children.Add(new Element
                    {
                        Name = "*",
                        Icon = null,
                        FullName = dir.FullName
                    });

                    observableCollection.Add(newItem);
                }

                foreach (FileInfo file in new DirectoryInfo(fileInfo.FullName).GetFiles().OrderBy(x => x.Name).ToArray())
                {

                    System.Drawing.Icon icon = (System.Drawing.Icon)System.Drawing.Icon.ExtractAssociatedIcon(file.FullName.ToString());


                    if (file.Extension.ToLower() == ".jpg" || file.Extension.ToLower() == ".jpeg" || file.Extension.ToLower() == ".bmp" || file.Extension.ToLower() == ".png")
                    {
                        observableCollection.Add(new CustomImage
                        {
                            Name = file.ToString(),
                            Icon = icon.ToImageSource(),
                            FullName = file.FullName.ToString(),
                            Size = ByteSize.FromBytes(file.Length).ToString(),
                            CreationTime = file.CreationTime.ToString(CultureInfo.InvariantCulture)
                        });
                    }
                    else
                    {
                        observableCollection.Add(new CustomFile
                        {
                            Name = file.ToString(),
                            Icon = icon.ToImageSource(),
                            FullName = file.FullName,
                            Size = ByteSize.FromBytes(file.Length).ToString(),
                            CreationTime = file.CreationTime.ToString(CultureInfo.InvariantCulture)
                        });
                    }



                }

            }
            catch (UnauthorizedAccessException ex)
            {
                Folder newIFolder = new Folder()
                {
                    Name = "Отказано в доступе"
                };
                observableCollection.Add(newIFolder);
            }
            item.ItemsSource = observableCollection;

        }



    }
}





