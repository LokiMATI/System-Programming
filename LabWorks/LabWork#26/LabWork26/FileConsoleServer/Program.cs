using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        var byteSize = new byte[8];
        var recieved = stream.Read(byteSize);

        var size = BitConverter.ToInt64(byteSize);

        var data = new byte[size];
        recieved = stream.Read(data);


        var result = Compress(data);
        size = result.Length;
        byteSize = BitConverter.GetBytes(size);
        data = new byte[size + 8];

        Array.Copy(byteSize, 0, data, 0, 8);
        Array.Copy(result, 0, data, 8, size);

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

byte[] Compress(byte[] data)
{
    using MemoryStream inputStream = new(data);
    using Bitmap originalImage = new(inputStream);

    int newWidth = originalImage.Width / 2;
    int newHeight = originalImage.Height / 2;

    using Bitmap resizedImage = new(newWidth, newHeight);
    using (var graphics = Graphics.FromImage(resizedImage))
    {
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
    }

    using MemoryStream outputStream = new();
    resizedImage.Save(outputStream, ImageFormat.Jpeg);
    return outputStream.ToArray();
}