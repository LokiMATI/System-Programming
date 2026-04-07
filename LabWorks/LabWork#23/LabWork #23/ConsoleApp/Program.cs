using System.CommandLine;
using System.Drawing;

int filesCurrentCountComplited = 0;

RootCommand root = new();
Option<DirectoryInfo> pathOption = new("--folder");
root.Options.Add(pathOption);

root.SetAction(async parseResult =>
{
    var directory = parseResult.GetValue(pathOption);
    InversionImages(directory);

});

var parseResulte = root.Parse(args);
return parseResulte.Invoke();

void InversionImages(DirectoryInfo directory)
{
    var files = directory.GetFiles("*.jpg");
    int lastFileCount = 0;
    Thread progressBar = new(() =>
    {
        while (lastFileCount < files.Length)
        {
            if (lastFileCount != filesCurrentCountComplited)
            {
                Console.Clear();
                lastFileCount = filesCurrentCountComplited;
                var progress = lastFileCount / (float)files.Length * 100.0;
                Console.Write("[");

                for (int i = 1; i < 100; i++)
                {
                    if (progress >= i)
                        Console.Write("#");
                    else
                        Console.Write(".");
                }

                Console.Write("]");
            }
        }
    });
    progressBar.Start();

    var result = Parallel.ForEach(files, InversionImage);
    while (!result.IsCompleted) { }
}



void InversionImage(FileInfo file)
{
    using Bitmap bitmap = new(file.FullName);
    var size = bitmap.Size;

    var width = bitmap.Width;
    var height = bitmap.Height;

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            var color = bitmap.GetPixel(x, y);
            bitmap.SetPixel(x, y,
                Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B));
        }
    }

    var path = Path.Combine(file.Directory.FullName, "Inversion");
    if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

    bitmap.Save(Path.Combine(path, $@"{file.Name}"));
    Interlocked.Increment(ref filesCurrentCountComplited);
}