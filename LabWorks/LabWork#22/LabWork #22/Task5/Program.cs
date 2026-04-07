CancellationTokenSource cts = new();
var cancellationToken = cts.Token;

Thread fileLoaderThread = new(() =>
{
    for (int i = 0; i <= 10; i++)
    {
        Console.WriteLine($"Файл загружен на {i}0%");
        Thread.Sleep(3000);
        if (cancellationToken.IsCancellationRequested)
            break;
    }
    cts.Cancel();
});

Thread cancellThread = new(() =>
{
    while (!cancellationToken.IsCancellationRequested) {
        var command = Console.ReadLine();

        if (command == "C")
            cts.Cancel();
    }
});


fileLoaderThread.Start();
cancellThread.Start();

fileLoaderThread.Join();
