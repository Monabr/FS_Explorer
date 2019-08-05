using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace FS_Explorer.Views
{

    //public class CustomInfo
    //{
    //    public ImageSource Icon { get; set; }
    //    public string Name { get; set; }
    //    public string FullName { get; set; }
    //    public string TypeOfFile { get; set; }
    //    public List<CustomInfo> Children { get; } = new List<CustomInfo>();
    //}



    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    foreach (DriveInfo d in DriveInfo.GetDrives())
        //    {
        //        Drivers.Items.Add(d.Name);
        //    }
        //} DONE

        //private void Drivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{


        //    Folders.Items.Clear();


        //    foreach (DirectoryInfo dir in new DirectoryInfo(Drivers.SelectedItem.ToString()).GetDirectories().OrderBy(x => x.Name).ToArray())
        //    {
                
        //        ShellObject shellFolder = ShellObject.FromParsingName(dir.FullName.ToString());
        //        BitmapSource shellThumb = shellFolder.Thumbnail.SmallBitmapSource;

        //        CustomInfo newItem = new CustomInfo
        //        {
        //            Name = dir.ToString(),
        //            Icon = shellThumb,
        //            FullName = dir.FullName,
        //            TypeOfFile = "Folder"
        //        };

        //        newItem.Children.Add(new CustomInfo
        //        {
        //            Name = "*",
        //            Icon = null,
        //            FullName = dir.FullName
        //        });

        //        Folders.Items.Add(newItem);

        //    }


        //    foreach (FileInfo file in new DirectoryInfo(Drivers.SelectedItem.ToString()).GetFiles().OrderBy(x => x.Name).ToArray())
        //    {
                
        //        Icon icon = (System.Drawing.Icon)System.Drawing.Icon.ExtractAssociatedIcon(file.FullName.ToString());

        //        Folders.Items.Add(
        //            new CustomInfo
        //            {
        //                Name = file.ToString(),
        //                Icon = icon.ToImageSource(),
        //                FullName = file.FullName.ToString(),
        //                TypeOfFile = "File"
        //            }
        //            );
        //    }




        //}

        //private void Folders_Expanded(object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem item = (TreeViewItem)e.OriginalSource;
        //    item.ClearValue(ItemsControl.ItemsSourceProperty);

        //    ObservableCollection<CustomInfo> observableCollection = new ObservableCollection<CustomInfo>();

        //    CustomInfo fileInfo = (CustomInfo)item.DataContext;
        //    content.Content = fileInfo.FullName;


        //    try
        //    {
        //        foreach (DirectoryInfo dir in new DirectoryInfo(fileInfo.FullName).GetDirectories().OrderBy(x => x.Name).ToArray())
        //        {
        //            ShellObject shellFolder = ShellObject.FromParsingName(dir.FullName.ToString());
        //            BitmapSource shellThumb = shellFolder.Thumbnail.SmallBitmapSource;

        //            CustomInfo newItem = new CustomInfo
        //            {
        //                Name = dir.ToString(),
        //                Icon = shellThumb,
        //                FullName = dir.FullName,
        //                TypeOfFile = "Folder"
        //            };

                    
        //            newItem.Children.Add(new CustomInfo
        //            {
        //                Name = "*",
        //                Icon = null,
        //                FullName = dir.FullName
        //            });
                    
        //            observableCollection.Add(newItem);
        //        }

        //        foreach (FileInfo file in new DirectoryInfo(Drivers.SelectedItem.ToString()).GetFiles().OrderBy(x => x.Name).ToArray())
        //        {

        //            System.Drawing.Icon icon = (System.Drawing.Icon)System.Drawing.Icon.ExtractAssociatedIcon(file.FullName.ToString());

        //            CustomInfo newItem = new CustomInfo
        //            {
        //                Name = file.ToString(),
        //                Icon = icon.ToImageSource(),
        //                FullName = file.FullName,
        //                TypeOfFile = "File"
        //            };
        //            observableCollection.Add(newItem);
        //        }

        //    }
        //    catch (Exception ex)
        //    { }

        //    item.ItemsSource = observableCollection;
        //}

        //private void Item_Selected(object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem item = (TreeViewItem)e.OriginalSource;
        //    CustomInfo fileInfo = (CustomInfo)item.DataContext;
            
        //    DirectoryInfo dir;
        //    System.IO.FileInfo file;
        //    // = (DirectoryInfo)item.Tag;

        //    if (item.Tag is DirectoryInfo)
        //    {
        //        dir = (DirectoryInfo)item.Tag;
        //        Process.Start(dir.FullName);
        //    }
        //    else
        //    {
        //        file = (System.IO.FileInfo)item.Tag;
        //        //content.Content = file.FullName;
        //        Process.Start(fileInfo.FullName);
        //    }





        //    //System.Diagnostics.Process.Start(dir.FullName);
        //}


    }
}
