internal class Program
{
    static string commonVar = "default";
    private static void Main()
    {
        Thread Main = new(() =>
        {
            while (commonVar == "default")
            {
                Console.WriteLine($"Значение commonVar: {commonVar}");
            }
            Console.WriteLine("Поток Main завершается");
        });

        Thread MyThread = new(() =>
        {
            Thread.Sleep(5000);
            commonVar = "x";
        });

        Main.Start();
        MyThread.Start();
    }
}
