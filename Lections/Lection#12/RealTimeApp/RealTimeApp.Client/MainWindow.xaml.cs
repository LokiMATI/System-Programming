using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Windows;

namespace RealTimeApp.Client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    HubConnection connection;

    public MainWindow()
    {
        InitializeComponent();

        connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5082/chat")
            .Build();
        connection.On<string, string>("Receive", (username, message) =>
        {
            Dispatcher.Invoke(() =>
            {
                var text = $"{username}: {message}";
                MessageList.Items.Add(text);
            });
        });
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await connection.InvokeAsync("Send", UsernameTextBox.Text, MessageTextBox.Text);
        }
        catch (Exception ex)
        {
            MessageList.Items.Add(ex.Message);
        }
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            MessageList.Items.Add(ex.Message);
        }
    }
}