using System.Threading;
using System.Threading.Tasks;

/*
Thread current = Thread.CurrentThread;

Console.WriteLine(current.ManagedThreadId);

Thread.Sleep(500);

Thread printThreed = new(() => Console.WriteLine("Print"));

printThreed.Start();
*/

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