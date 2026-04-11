using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ip = new(IPAddress.Any, 8888);
TcpListener tcpListener = new(ip);
List<TcpClient> clients = new();

try
{
    tcpListener.Start();
    Console.WriteLine("Жду соединений");

    void Sender(object o)
    {
        byte[] data = Encoding.UTF8.GetBytes("Привет, Ким чан ын");
        clients.ForEach(c =>
        {
            var s = c.GetStream();
            s.Write(data);
        });
    }

    Timer timer = new(Sender, null, 0, 1000);

    while (true)
    {
        var tcpClient = tcpListener.AcceptTcpClient();
        Console.WriteLine(tcpClient.Client.RemoteEndPoint);
        clients.Add(tcpClient);
    }
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    tcpListener.Stop();
}