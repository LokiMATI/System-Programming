using System.Net;
using System.Net.Sockets;
using System.Text;

//using Socket listiner = new(
//    SocketType.Stream, 
//    ProtocolType.Tcp);

//IPEndPoint ip = new(IPAddress.Any, 8888);

//try
//{
//    listiner.Bind(ip);
//    listiner.Listen();
//    Console.WriteLine("Жду соединений");

//    while (true)
//    {
//        using var client = listiner.Accept();
//        byte[] data = Encoding.UTF8.GetBytes("Привет, Ким чан ын");
//        client.Send(data);
//        Console.WriteLine($"Клиенту {client.RemoteEndPoint} отправлены данные");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

IPEndPoint ip = new(IPAddress.Any, 8888);
TcpListener tcpListener = new(ip);

try
{
    tcpListener.Start();
    Console.WriteLine("Жду соединений");

    while (true)
    {
        using var tcpClient = tcpListener.AcceptTcpClient();
        Console.WriteLine(tcpClient.Client.RemoteEndPoint);
        var stream = tcpClient.GetStream();
        byte[] data = Encoding.UTF8.GetBytes("Привет, Ким чан ын");
        stream.Write(data);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    tcpListener.Stop();
}