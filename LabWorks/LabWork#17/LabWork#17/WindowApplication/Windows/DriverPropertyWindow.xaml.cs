using System.IO;
using System.Windows;

namespace WindowApplication.Windows;

/// <summary>
/// Логика взаимодействия для DriverProperty.xaml
/// </summary>
public partial class DriverPropertyWindow : Window
{
    public DriverPropertyWindow(DriveInfo info)
    {
        InitializeComponent();

        DriveTitleTextBlock.Text = info.Name;
        DriveTypeTextBlock.Text = info.DriveType.ToString();
        DriveFileSystemTextBlock.Text = info.DriveFormat.ToString();
        DriveOccupiedSizeTextBlock.Text = $"{info.TotalSize - info.TotalFreeSpace} байт";
        DriveFreeSpaceTextBlock.Text = $"{info.TotalFreeSpace} байт";
        DriveTotalTextBlock.Text = $"{info.TotalSize} байт";
    }
}
