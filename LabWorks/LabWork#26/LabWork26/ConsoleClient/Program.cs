using System.Net;
using System.Net.Sockets;
using System.Text;

using UdpClient udpClient = new();
var ip = IPEndPoint.Parse("127.0.0.1:5555");

while (true)
{
    Console.Write("Введите сообщение: ");
    var data = Encoding.UTF8.GetBytes(Console.ReadLine());

    int bytes = await udpClient.SendAsync(data, ip);
}
