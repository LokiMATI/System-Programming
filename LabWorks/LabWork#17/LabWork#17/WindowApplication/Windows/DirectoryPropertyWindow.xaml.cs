using System.IO;
using System.Windows;

namespace WindowApplication.Windows;

/// <summary>
/// Логика взаимодействия для DirectoryPropertyWindow.xaml
/// </summary>
public partial class DirectoryPropertyWindow : Window
{
    public DirectoryPropertyWindow(DirectoryInfo directory)
    {
        InitializeComponent();
        DirectoryNameTextBlock.Text = directory.Name;

        var files = directory.GetFiles("*", SearchOption.AllDirectories);
        FilesCountTextBlock.Text = $"{files.Length} файлов";

        var directoriesCount = directory.GetDirectories("*", SearchOption.AllDirectories).Length;
        DirectoryCountTextBlock.Text = $"{directoriesCount} папок";

        var dirSize = files.Sum(f => f.Length);
        SizeTextBlock.Text = $"{dirSize} байт размер папки";

        var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.Name == directory.FullName.Substring(0, 3));
        var procent = dirSize / (float)drive.TotalSize * 100.0;
        ProcentOccupatedTextBlock.Text = $"{procent.ToString("F2")}%";

        var topFiles = files.OrderBy(f => f.Length).Take(5).ToList();
        TopFilesList.ItemsSource = topFiles;
    }
}
