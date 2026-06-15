using System.Windows;

namespace WindowApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
            string.IsNullOrWhiteSpace(RoomTextBox.Text))
        {
            MessageBox.Show("Логин или название комнаты не может быть пустым или состоять из пробелов", "Ошибка захода", MessageBoxButton.OK);
            return;
        }

        ChatWindow window = new(RoomTextBox.Text.Trim(), LoginTextBox.Text.Trim());
        window.Show();
        Close();
    }
}