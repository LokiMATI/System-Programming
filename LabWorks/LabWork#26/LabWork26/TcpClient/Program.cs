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
    while (true)
    {
        int recieved = stream.Read(buffer);

        if (recieved == 0)
            continue;

        var message = Encoding.UTF8.GetString(buffer, 0, recieved);
        buffer = new byte[512];
        Console.WriteLine(message);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    tcpClient.Close();
}
