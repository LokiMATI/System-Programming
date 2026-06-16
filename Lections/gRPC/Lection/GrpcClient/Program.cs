using Grpc.Net.Client;
using GrpcClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5238");

var client = new Greeter.GreeterClient(channel);


Console.Write("Введтие имя: ");
string? name = Console.ReadLine();

var reply = await client.SayHelloAsync(new HelloRequest
{
    Name = name
});

Console.WriteLine($"Ответ сервера: {reply.Message}");
Console.ReadLine();