using System.Net.Sockets;
using System.Text;

using UdpClient udpServer = new(5555);
Console.WriteLine("Сервер запущен");

while (true)
{
    var result = await udpServer.ReceiveAsync();

    var message = Encoding.UTF8.GetString(result.Buffer);
    Console.WriteLine($"{result.RemoteEndPoint} [{DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy")}]: {message}");
}