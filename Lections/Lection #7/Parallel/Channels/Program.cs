/*
ConcurrentQueue<int> queue = new();

var producer1 = Task.Run(() =>
{
    for (int i = 0; i < 5; i++)
    {
        queue.Enqueue(i);
        Console.WriteLine($"Добавлено {i}");
        Thread.Sleep(500);
    }
});

var producer2 = Task.Run(() =>
{
    for (int i = 0; i < 5; i++)
    {
        queue.Enqueue(i);
        Console.WriteLine($"Добавлено {i}");
        Thread.Sleep(500);
    }
});

var comsumer = Task.Run(() =>
{
    while (true)
    {
        if(queue.TryDequeue(out var item))
        {
            Console.WriteLine($"Извлечено {item}");
        }
        else
        {
            Console.WriteLine("Ожидаю...");
        }
        Thread.Sleep(300);
    }
});

Task.WaitAll(producer1, producer2, comsumer);
*/

/*
BlockingCollection<int> collection = new();

var producer = Task.Run(() =>
{
    for (int i = 0; i < 5; i++)
    {
        collection.Add(i);
        Console.WriteLine($"Добавлено {i}");
        Thread.Sleep(500);
    }
    collection.CompleteAdding();
});

var comsumer = Task.Run(() =>
{
    Thread.Sleep(3000);
    foreach (var item in collection.GetConsumingEnumerable())
    {
        Console.WriteLine($"Извлечено {item}");
    }
});

Task.WaitAll(producer, comsumer);
*/

/*
using System.Threading.Channels;

var channel = Channel.CreateUnbounded<int>();

var writer = Task.Run(async () =>
{
    for (int i = 0; i < 5; i++)
    {
        await channel.Writer.WriteAsync(i);
        Console.WriteLine($"Добавлено {i}");
        await Task.Delay(500);
    }
    channel.Writer.Complete();
});

var reader = Task.Run(async () =>
{
    await foreach (var item in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"Извлечено {item}");
    }
});

await Task.WhenAll(writer, reader);
*/



using System.Collections.Concurrent;
using System.IO.Pipes;

using NamedPipeServerStream server = new("MyChannel");
using StreamWriter sw = new(server);

server.WaitForConnection();

using NamedPipeClientStream client = new("MyChannel");
using StreamReader sr = new(client);

client.Connect();

