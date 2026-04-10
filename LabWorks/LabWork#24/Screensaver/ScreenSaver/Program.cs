namespace ScreenSaver;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        if (args.Length == 0) return;

        var firstArg = args[0];
        switch (firstArg)
        {
            case "/c":
                break;
            case "/p":
                break;
            case "/s":
                Application.Run(new TimerScreenSaver());
                break;
        }
    }
}