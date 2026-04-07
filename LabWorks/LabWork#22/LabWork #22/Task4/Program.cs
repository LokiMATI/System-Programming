//using System.Threading;

//int counter = 0;

//var threads = new Thread[5];

//for (int i = 0; i < 5; i++)
//{
//    threads[i] = new(() =>
//    {
//        for (int j = 0; j < 1000; j++)
//        {
//            counter++;
//        } 
//    });
//}

//foreach (var thread in threads)
//{
//    thread.Start();
//}

//foreach (var thread in threads)
//{
//    thread.Join();
//}

//Console.WriteLine(counter);


int x = 0;
object lockObject = new();


for (int i = 1; i < 6; i++)
{
    Thread myThread = new(Print);
    myThread.Name = $"Поток {i}";
    myThread.Start();
}

void Print()
{
    for (int i = 0; i < 1000; i++)
    {
        lock (lockObject)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {++x}");
        }
        Thread.Sleep(1);
    }
}
