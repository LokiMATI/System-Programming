using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ip = new(IPAddress.Parse("127.0.0.1"), 8888);
TcpClient tcpClient = new();

try
{
    tcpClient.Connect(ip);
    var buffer = new byte[512];
    var stream = tcpClient.GetStream();
    int recieved = stream.Read(buffer);

    var message = Encoding.UTF8.GetString(buffer, 0, recieved);

    Console.WriteLine(message);
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    tcpClient.Close();
}