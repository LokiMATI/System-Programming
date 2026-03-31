using System.CommandLine;
using System.Security.Cryptography;
using System.Text;

RootCommand root = new();

Option<DirectoryInfo> directoriOption = new("--dir");
Option<bool> recursiveOption = new("-r");
Option<bool> deleteDublicateOption = new("-d");


root.Options.Add(directoriOption);
root.Options.Add(recursiveOption);
root.Options.Add(deleteDublicateOption);

root.SetAction(parseResult =>
{
    DirectoryInfo directory = parseResult.GetValue(directoriOption);
    bool isRecursiveMode = parseResult.GetValue(recursiveOption);
    bool isDeleteMode = parseResult.GetValue(deleteDublicateOption);
    SeachDublicate(directory, isRecursiveMode, isDeleteMode);
    return 0;
});

ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();

string? GetFileHash(string path)
{
    if (!File.Exists(path))
        return null;

    using (var md5 = MD5.Create())
    {
        using (var stream = File.OpenRead(path))
        {
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}

void SeachDublicate(DirectoryInfo directory, bool isRecursiveMode, bool isDeleteMode)
{
    var files = directory.GetFiles("*", isRecursiveMode ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

    StringBuilder stringBuilder = new();

    string? hash;
    string? includeHash;
    bool isWrite;
    List<FileInfo> deleteFiles = new(files.Length - 1);
    List<FileInfo> dublicateFiles = new(files.Length - 1);
    for (int i = 0; i < files.Length; i++)
    {
        isWrite = false;
        hash = GetFileHash(files[i].FullName);
        if (hash is null)
            continue;

        for (int j = i + 1; j < files.Length; j++) 
        {
            if (dublicateFiles.Find(f => f == files[j]) is not null)
                continue;

            includeHash = GetFileHash(files[j].FullName);
            if (includeHash is null || hash != includeHash)
                continue;

            dublicateFiles.Add(files[j]);

            if (!isWrite)
            {
                stringBuilder.AppendLine(files[i].FullName);
                isWrite = true;
            }

            stringBuilder.AppendLine(files[j].FullName);

            if (isDeleteMode)
            {
                deleteFiles.Add(files[j]);
            }
        }
    }

    if (isDeleteMode)
        foreach (var file in deleteFiles)
            File.Delete(file.FullName);

    if (stringBuilder.Length < 0)
    {
        Console.WriteLine("Дубликаты не были найены.");
        return;
    }

    Console.WriteLine($"Были найдены дубликаты:\n{stringBuilder.ToString()}");
}
