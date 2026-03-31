//using System.Threading;

//var timer = new Timer(Callback, null, 0, 1000);

//Console.ReadLine();
//timer.Dispose();

//void Callback(object state)
//{
//    Console.WriteLine(DateTime.Now);
//}

//using System.Timers;

//var timer = new System.Timers.Timer(1000);

//timer.Elapsed += Timer_Elapsed;
//timer.Start();

//Console.ReadLine();
//void Timer_Elapsed(object? sender, ElapsedEventArgs e)
//{
//    Console.WriteLine(DateTime.Now);
//}

using System.Threading.Tasks;
var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

while (await timer.WaitForNextTickAsync())
{
    Console.WriteLine(DateTime.Now);
}