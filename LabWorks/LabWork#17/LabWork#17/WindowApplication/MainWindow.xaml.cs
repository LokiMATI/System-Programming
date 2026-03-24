using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;
using System.Collections.Specialized;
using System.IO.Compression;

namespace WindowApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ObjectInfo> Objects = new();
        public MainWindow()
        {
            InitializeComponent();
            LoadDirectory();

        }

        private void LoadDirectory()
        {
            Objects.Clear();
            FilesAndDirictoriesListBox.ItemsSource = null;
            DirectoryInfo directory = new(DirectoryTextBox.Text);
            if (directory.Exists)
            {
                Objects.Add(new ObjectInfo()
                {
                    ObjectName = "...",
                    ImagePath = @"/Icons/folder.png",
                    Type = ObjectType.Directory
                });
                Objects.AddRange(directory.GetFiles().Select(f => new ObjectInfo(f)));
                Objects.AddRange(directory.GetDirectories().Select(d => new ObjectInfo(d)));
            }
            FilesAndDirictoriesListBox.ItemsSource = Objects;
        }

        private void FilesAndDirictoriesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedObject = FilesAndDirictoriesListBox.SelectedItem as ObjectInfo;

            if (selectedObject.Type == ObjectType.Directory)
            {
                string path = DirectoryTextBox.Text;
                if (selectedObject.ObjectName == "...") 
                {
                    DirectoryInfo info = new(DirectoryTextBox.Text);

                    if (info.Exists)
                        path = info.Parent.FullName;
                }
                else
                {
                    path = Path.Combine(DirectoryTextBox.Text, selectedObject.ObjectName);
                }
                DirectoryTextBox.Text = path;
                LoadDirectory();
            }
            else
            {
                var path = Path.Combine(DirectoryTextBox.Text, selectedObject.ObjectName);
                if (File.Exists(path))
                {
                    ProcessStartInfo info = new ("explorer", path);
                    Process.Start(info);
                }
                   
            }
        }

        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (FilesAndDirictoriesListBox.SelectedItems.Count == 0)
                return;

            var objects = FilesAndDirictoriesListBox.SelectedItems.Cast<ObjectInfo>().Where(o => o.Type == ObjectType.File);
            StringCollection stringCollection = new();
            var paths = objects.Select(o => Path.Combine(DirectoryTextBox.Text, o.ObjectName));
            stringCollection.AddRange(paths.ToArray());

            Clipboard.SetFileDropList(stringCollection);
        }

        private void PasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var stringCollection = Clipboard.GetFileDropList();
            FileInfo info;
            foreach (var path in stringCollection)
            {
                if (File.Exists(path))
                {
                    info = new(path);
                    info.CopyTo(Path.Combine(DirectoryTextBox.Text, info.Name));
                }
            }

            LoadDirectory();

        }

        private void ArchiveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (FilesAndDirictoriesListBox.SelectedItems.Count == 0)
                return;

            var folder = FilesAndDirictoriesListBox.SelectedItems.Cast<ObjectInfo>().Where(o => o.Type == ObjectType.Directory).First();

            var source = Path.Combine(DirectoryTextBox.Text, folder.ObjectName);
            ZipFile.CreateFromDirectory(source, source + ".zip");

            LoadDirectory();
        }

        private void DearchiveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (FilesAndDirictoriesListBox.SelectedItems.Count == 0)
                return;

            var file = FilesAndDirictoriesListBox.SelectedItems.Cast<ObjectInfo>().Where(o => o.Type == ObjectType.File).First();

            var path = Path.Combine(DirectoryTextBox.Text, file.ObjectName);
            FileInfo info = new(path);

            if (info.Extension == ".zip")
                return;

            ZipFile.ExtractToDirectory(path, DirectoryTextBox.Text);
            LoadDirectory();
        }
    }
}

