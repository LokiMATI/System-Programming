using System.CommandLine;

RootCommand root = new();

Option<DirectoryInfo> directoriOption = new("--dir");
Option<string?> sortTypeOption = new("--type")
{
    Required = false
};

root.Options.Add(directoriOption);
root.Options.Add(sortTypeOption);

root.SetAction(parseResult =>
{
    DirectoryInfo directory = parseResult.GetValue(directoriOption);
    var sortType = parseResult.GetValue(sortTypeOption);

    switch (sortType)
    {
        case "date":
            SortByDateDirectory(directory);
            break;

        case "ext":
            SortByExtensionDirectory(directory);
            break;

        case null:
            ClearSubDirectories(directory);
            break;

        default:
            Console.WriteLine("Неизвестный тип сортировки");
            break;
    }
    
    return 0;
});

ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();

void SortByDateDirectory(DirectoryInfo directory)
{
    var files = directory.GetFiles();

    foreach (var file in files)
    {
        var date = file.LastWriteTime;
        var path = Path.Combine(directory.FullName, @$"{date.ToString("yyyy")}\{date.ToString("MM")}\{date.ToString("dd")}");

        if (!File.Exists(path))
            Directory.CreateDirectory(path);

        file.MoveTo(Path.Combine(path, file.Name));
    }
}

void SortByExtensionDirectory(DirectoryInfo directory)
{
    var files = directory.GetFiles();

    foreach (var file in files)
    {
        var extension = file.Extension;
        if (string.IsNullOrWhiteSpace(extension))
            extension = "Empty";
        else
            extension = extension.Substring(1).ToUpper();

        var path = Path.Combine(directory.FullName, extension);

        if (!File.Exists(path))
            Directory.CreateDirectory(path);

        file.MoveTo(Path.Combine(path, file.Name));
    }
}

void ClearSubDirectories(DirectoryInfo directory)
{
    var files = directory.GetFiles("*", SearchOption.AllDirectories);

    foreach (var file in files)
        file.MoveTo(Path.Combine(directory.FullName, file.Name));

    var directories = directory.GetDirectories();
    foreach (var subdirectory in directories)
        Directory.Delete(subdirectory.FullName, true);
}
