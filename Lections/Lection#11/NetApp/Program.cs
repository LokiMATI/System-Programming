using System.Net;
using System.Net.NetworkInformation;

IPAddress local = new(new byte[] {127, 0, 0, 1});

Console.WriteLine(local);

IPAddress ip = IPAddress.Parse("127.0.0.1");

Console.WriteLine(ip);

Console.WriteLine(IPAddress.Loopback);
Console.WriteLine(IPAddress.Broadcast);
Console.WriteLine(IPAddress.Any);

Console.WriteLine(IPAddress.Loopback.AddressFamily);

IPEndPoint endPoint = new IPEndPoint(ip, 80);
Console.WriteLine(endPoint);

IPEndPoint point = IPEndPoint.Parse("127.0.0.1:8080");
Console.WriteLine(point);

Uri uri = new("https://google.com");

var data = Dns.GetHostEntry("arcotel.ru");
Console.WriteLine(data.HostName);

foreach (IPAddress address in data.AddressList)
{
    Console.WriteLine(address);
}

Console.WriteLine(Dns.GetHostName());

var adapters = NetworkInterface.GetAllNetworkInterfaces();

foreach (NetworkInterface adapter in adapters)
{
    Console.WriteLine(adapter.Name);
    Console.WriteLine(adapter.Speed);
    Console.WriteLine(adapter.NetworkInterfaceType);
    Console.WriteLine();
}

var properties = IPGlobalProperties.GetIPGlobalProperties();
var connections = properties.GetActiveTcpConnections();
foreach (var connection in connections)
{
    Console.WriteLine(connection.RemoteEndPoint);
}