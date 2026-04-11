using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ip = new(IPAddress.Parse("127.0.0.1"), 8888);
TcpClient tcpClient = new();

try
{
    Console.Write("Укаите путь до файла-изображения: ");
    var path = Console.ReadLine();

    FileInfo file = new(path);
    if (!file.Exists)
    {
        Console.WriteLine("Такого файла нет.");
        return;
    }

    if (file.Extension != ".jpg")
    {
        Console.WriteLine("Файл не является изображением.");
        return;
    }

    tcpClient.Connect(ip);
    var stream = tcpClient.GetStream();
    var byteSize = BitConverter.GetBytes(file.Length);
    var data = File.ReadAllBytes(path);

    var buffer = new byte[file.Length + 8];
    Array.Copy(byteSize, 0, buffer, 0, 8);
    Array.Copy(data, 0, buffer, 8, file.Length);

    stream.Write(buffer);

    while (true)
    {
        byteSize = new byte[8];
        var recieved = stream.Read(byteSize);

        if (recieved == 0)
            continue;

        var size = BitConverter.ToInt64(byteSize);

        data = new byte[size];
        recieved = stream.Read(data);

        path = Path.Combine(file.DirectoryName, "Response");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, file.Name);
        File.WriteAllBytes(path, data);
        break;
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
