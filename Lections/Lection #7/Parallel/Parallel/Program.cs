/*
Thread current = Thread.CurrentThread;

Console.WriteLine(current.ManagedThreadId);

Thread.Sleep(500);

Thread printThreed = new(() => Console.WriteLine("Print"));

printThreed.Start();
*/

/*
Task task = new(() => Console.WriteLine("1"));
task.Start();

Task task2 = Task.Factory.StartNew(() => Console.WriteLine("2"));

Task task3 = Task.Run(() => Console.WriteLine("3"));

task.Wait();

var tasks = new Task[5];

for (int i = 0; i < 5; i++)
{
    tasks[i] = Task.Run(() => Console.WriteLine(i));
}

Task.WaitAll(tasks);

Task<int> taskRes = new(() =>
{
    Thread.Sleep(1000);
    return 10;
});

taskRes.Start();

Console.WriteLine(taskRes.Result);
*/

Parallel.Invoke(
    Print,
    () => { Console.WriteLine("Привет, Слава"); },
    () => PrintHello("Никита")
    );

Parallel.For(1, 11, Square);

List<Point> list = [new(1, 1), new(2, 2), new(3, 3)];

Parallel.ForEach(list, (item) => { item.X = 5; });

foreach (var point in list)
    Console.WriteLine($"{point.X}, {point.Y}");

void Print()
{
    Console.WriteLine("Привет Ким");
}

void PrintHello(string name)
{
    Console.WriteLine($"Привет, {name}");
}

void Square(int n)
{
    Console.WriteLine(n * n);
    Thread.Sleep(1000);
}

class Point(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}