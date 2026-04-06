using System.Net;
using System.Net.Sockets;
using System.Text;

using UdpClient udpClient = new();

while (true)
{
    var data = Encoding.UTF8.GetBytes(Console.ReadLine());

    var ip = IPEndPoint.Parse("127.0.0.1:5555");

    int bytes = await udpClient.SendAsync(data, ip);
}
