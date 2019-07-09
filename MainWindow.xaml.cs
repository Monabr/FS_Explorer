using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                TreeViewItem newItem = new TreeViewItem
                {
                    Tag = dir,
                    Header = dir.ToString()
                };
                newItem.Items.Add("*");
                Folders.Items.Add(newItem);
            }

            
            foreach (FileInfo file in new DirectoryInfo(Drivers.SelectedItem.ToString()).GetFiles())
            {
                TreeViewItem newItem = new TreeViewItem
                {
                    Tag = file,
                    Header = file.ToString()
                };
                Folders.Items.Add(newItem);
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
