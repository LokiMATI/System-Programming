using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using System.Windows;

namespace WindowApp;

/// <summary>
/// Логика взаимодействия для ChatWindow.xaml
/// </summary>
public partial class ChatWindow : Window
{
    private string _login;
    private string _room;
    private StringBuilder _chat = new();
    private HubConnection _connection;
    public ChatWindow(string room, string login)
    {
        InitializeComponent();
        _room = room;
        _login = login;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5153/chat")
            .Build();
        _connection.On("ReceiveMessage", (string user, string message) =>
        {
            Dispatcher.Invoke(() =>
            {
                _chat.AppendLine($"{user}: {message}");
                ChatTextBlock.Text = _chat.ToString();
            });
        });

        await _connection.StartAsync();
        await _connection.InvokeAsync("JoinRoom", _room, _login);
    }

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(MessageTextBox.Text))
        {
            MessageBox.Show("Поле сообщения не должно быть пустым.", "Ошибка отправки");
            return;
        }
        await _connection.InvokeAsync("Send", MessageTextBox.Text.Trim());
        MessageTextBox.Text = string.Empty;
    }
}
