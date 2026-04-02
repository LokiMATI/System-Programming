using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
namespace LabApp;

/// <summary>
/// Логика взаимодействия для StartProcessWindow.xaml
/// </summary>
public partial class StartProcessWindow : Window
{
    private MainWindow _processForm;
    public StartProcessWindow(MainWindow processForm)
    {
        InitializeComponent();
        _processForm = processForm;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (ProcessTextBox.Text.Trim() == string.Empty){
            MessageBox.Show("Поле имени процесса не должно быть пустым",
                "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!File.Exists(ProcessTextBox.Text)){
            MessageBox.Show("Данного процесса не существует",
                "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var file = new FileInfo(ProcessTextBox.Text);

        if (file.Extension.ToLower() != ".exe"){
            MessageBox.Show("Файл не являтся исполняемым",
                "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Process.Start(file.FullName);
        _processForm.UpdateProcessesListView();
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new();
        dialog.Filter = "Исполняемый файл|*.exe";

        if (dialog.ShowDialog() == true)
        {
            ProcessTextBox.Text = dialog.FileName;
        }
    }
}
