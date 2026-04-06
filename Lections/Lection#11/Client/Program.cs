using System.Net;
using System.Net.Sockets;
using System.Text;

//using Socket client = new(
//    SocketType.Stream,
//    ProtocolType.Tcp);

//IPEndPoint ip = new(IPAddress.Parse("127.0.0.1"), 8888);

//try
//{
//    client.Connect(ip);

//    byte[] buffer = new byte[512];
//    int recieved = client.Receive(buffer);

//    string message = Encoding.UTF8.GetString(buffer, 0, recieved);

//    Console.WriteLine(message);
//    Console.ReadLine();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

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
