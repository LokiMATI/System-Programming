namespace LabProject;

internal class FileLogger(string status) : IDisposable
{
    private string _status = status;

    StreamWriter _writer = new($"log_{status}.txt");
    public void Log()
    {
        _writer.WriteLine($"{DateTime.Now} [{status}]: лог");
    }

    public void Dispose()
    {
        _writer.Close();
        _writer.Dispose();
    }
}
