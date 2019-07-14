using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FS_Explorer
{

    public class ConvertBitmapToBitmapImage
    {
        /// <summary>
        /// Takes a bitmap and converts it to an image that can be handled by WPF ImageBrush
        /// </summary>
        /// <param name="src">A bitmap image</param>
        /// <returns>The image as a BitmapImage for WPF</returns>
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }

    public class MyFileInfo
    {
        public ImageSource Icon { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var sf = (ShellFolder)ShellObject.FromParsingName(
                            "shell:::{4234d49b-0245-4df3-b780-3893943456e1}");
            Debug.WriteLine(sf.Name);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                Drivers.Items.Add(d.Name);
            }
        }

        private void Drivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            Folders.Items.Clear();
   

            foreach (DirectoryInfo dir in new DirectoryInfo(Drivers.SelectedItem.ToString()).GetDirectories().OrderBy(x => x.Name).ToArray())
            {
                //ShellFile shellFile = ShellFile.FromFilePath(dir.FullName.ToString());
                ShellObject shellFolder = ShellObject.FromParsingName(dir.FullName.ToString());
                BitmapSource shellThumb = shellFolder.Thumbnail.SmallBitmapSource;


                //System.Drawing.Icon icon = (System.Drawing.Icon)System.Drawing.Icon.ExtractAssociatedIcon(dir.FullName);
                //TreeViewItem newItem = new TreeViewItem
                //{
                //    Tag = dir,
                //    Header = dir.ToString()
                //};


                MyFileInfo newItem = 
                    new MyFileInfo
                    {
                        Name = dir.ToString(),
                        Icon = shellThumb,
                        FullName = dir.FullName
                    }
                    ;

                //newItem.Items.Add("*");
                Folders.Items.Add(newItem);
            }

            
            foreach (FileInfo file in new DirectoryInfo(Drivers.SelectedItem.ToString()).GetFiles().OrderBy(x => x.Name).ToArray())
            {

                System.Drawing.Icon icon = (System.Drawing.Icon)System.Drawing.Icon.ExtractAssociatedIcon(file.FullName.ToString());
                //TreeViewItem newItem = new TreeViewItem 
                //{
                //    Tag = file,
                //    Header = file.ToString()

                //};
                Folders.Items.Add(
                    new MyFileInfo
                    {
                        Name = file.ToString(),
                        Icon = icon.ToImageSource(),
                        FullName = file.FullName.ToString()
                    }
                    );
            }




        }

        private void Folders_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            item.Items.Clear();
            DirectoryInfo dir;
            
            if (item.Tag is DriveInfo) {
                DriveInfo drive = (DriveInfo)item.Tag;
                dir = drive.RootDirectory;
            }
            else if (item.Tag is FileInfo)
            {
                dir = null;
            }
            else dir = (DirectoryInfo)item.Tag;
            try
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories().OrderBy(x => x.Name).ToArray())
                {
                    
                    TreeViewItem newItem = new TreeViewItem 
                    {
                        Tag = subDir,
                        Header = subDir.ToString()

                    };
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }

                foreach (FileInfo file in dir.GetFiles().OrderBy(x => x.Name).ToArray())
                {
                    TreeViewItem newItem = new TreeViewItem
                    {
                        Tag = file,
                        Header = file.ToString()
                    };
                    item.Items.Add(newItem);
                }

            }
            catch (Exception ex)
            {  }
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            DirectoryInfo dir;
            FileInfo file;
            // = (DirectoryInfo)item.Tag;

            if (item.Tag is DirectoryInfo)
            {
                dir = (DirectoryInfo)item.Tag;
                Process.Start(dir.FullName);
            }
            else {
                file = (FileInfo)item.Tag;
                content.Content = file.FullName;
                Process.Start(file.FullName);
            } 
            


           

            //System.Diagnostics.Process.Start(dir.FullName);
        }

       
    }
}
