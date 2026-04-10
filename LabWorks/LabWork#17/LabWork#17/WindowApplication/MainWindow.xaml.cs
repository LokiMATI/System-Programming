using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Path = System.IO.Path;
using System.Collections.Specialized;
using System.IO.Compression;
using WindowApplication.Windows;

namespace WindowApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ObjectManagerInfo> Objects = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadDirectory()
        {
            Objects.Clear();
            FilesAndDirictoriesListBox.ItemsSource = null;
            if (string.IsNullOrWhiteSpace(DirectoryTextBox.Text))
            {
                Objects.AddRange(DriveInfo.GetDrives().Select(d => new DriveManagerInfo(d)));
            }
            else
            {
                DirectoryInfo directory = new(DirectoryTextBox.Text);
                if (directory.Exists)
                {
                    Objects.Add(new DirectoryManagerInfo(directory)
                    {
                        Name = "..."
                    });
                    Objects.AddRange(directory.GetFiles().Select(f => new FileManagerInfo(f)));
                    Objects.AddRange(directory.GetDirectories().Select(d => new DirectoryManagerInfo(d)));
                }
            }
                
            FilesAndDirictoriesListBox.ItemsSource = Objects;
        }

        private void FilesAndDirictoriesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedObject = FilesAndDirictoriesListBox.SelectedItem as ObjectManagerInfo;

            if (selectedObject is DirectoryManagerInfo directory)
            {
                string path = DirectoryTextBox.Text;
                if (directory.Name == "...") 
                {
                    DirectoryInfo info = new(DirectoryTextBox.Text);

                    if (info.Exists && info.Parent is not null)
                        path = info.Parent.FullName;
                    else
                        path = "";
                }
                else
                {
                    path = Path.Combine(DirectoryTextBox.Text, directory.Name);
                }
                DirectoryTextBox.Text = path;
                LoadDirectory();
            }
            else if (selectedObject is FileManagerInfo file)
            {
                var path = Path.Combine(DirectoryTextBox.Text, file.Name);
                if (File.Exists(path))
                {
                    ProcessStartInfo info = new ("explorer", path);
                    Process.Start(info);
                }
            }
            else if (selectedObject is DriveManagerInfo drive)
            {
                if (!string.IsNullOrWhiteSpace(DirectoryTextBox.Text))
                    return;

                DirectoryTextBox.Text = Path.Combine(drive.Name);
                LoadDirectory();
            }
        }

        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (FilesAndDirictoriesListBox.SelectedItems.Count == 0)
                return;

            var objects = FilesAndDirictoriesListBox.SelectedItems.Cast<ObjectManagerInfo>().Where(o => o is FileManagerInfo);
            StringCollection stringCollection = new();
            var paths = objects.Select(o => Path.Combine(DirectoryTextBox.Text, o.Name));
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

            var folder = FilesAndDirictoriesListBox.SelectedItems.Cast<ObjectManagerInfo>().Where(o => o is DirectoryManagerInfo).First();

            var source = Path.Combine(DirectoryTextBox.Text, folder.Name);
            ZipFile.CreateFromDirectory(source, source + ".zip");

            LoadDirectory();
        }

        private void DearchiveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (FilesAndDirictoriesListBox.SelectedItems.Count == 0)
                return;

            var file = FilesAndDirictoriesListBox.SelectedItems.Cast<ObjectManagerInfo>().Where(o => o is FileManagerInfo).First();

            var path = Path.Combine(DirectoryTextBox.Text, file.Name);
            FileInfo info = new(path);

            if (info.Extension == ".zip")
                return;

            ZipFile.ExtractToDirectory(path, DirectoryTextBox.Text);
            LoadDirectory();
        }

        private void DirectoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDirectory();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDirectory();
        }

        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ObjectManagerInfo)FilesAndDirictoriesListBox.SelectedItem;

            if (selectedItem is DriveManagerInfo drive)
            {
                DriverPropertyWindow window = new(DriveInfo.GetDrives().First(d => d.Name == drive.Name));
                window.ShowDialog();
            }
            else if (selectedItem is DirectoryManagerInfo directory)
            {
                DirectoryPropertyWindow window = new(new DirectoryInfo(directory.FullName));
                window.ShowDialog();
            }
        }
    }
}

