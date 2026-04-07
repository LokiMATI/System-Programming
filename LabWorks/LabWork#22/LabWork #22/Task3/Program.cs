using System.Collections.Concurrent;
using System.Runtime.InteropServices;

internal class Program
{
    static ConcurrentQueue<int> NumberQuery = new();
    private static void Main(string[] args)
    {
        Thread producer = new(() =>
        {
            while (true)
            {
                NumberQuery.Enqueue(Random.Shared.Next(20));
                Thread.Sleep(700);
            }
        });
        producer.Start();

        for (int i = 0; i < 3; i++)
        {
            Thread consumer = new(() => ConsumerPrint(i));
            consumer.Start();
        }
    }

    public static void ConsumerPrint(int id)
    {
        while (true) 
        {
            int number;
            if (NumberQuery.TryDequeue(out number))
            {
                Console.WriteLine($"{id} потребитель достал число: {number}");
                Thread.Sleep(900);
            }
        }
    }
}
