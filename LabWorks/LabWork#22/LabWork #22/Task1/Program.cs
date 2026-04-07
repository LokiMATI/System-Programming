Thread print1 = new(() => {
    while (true)
    {
        Console.WriteLine("1");
        Thread.Sleep(1000);
    }
});

print1.Start();

while (true)
{
    Console.WriteLine("0");
    Thread.Sleep(700);
}
