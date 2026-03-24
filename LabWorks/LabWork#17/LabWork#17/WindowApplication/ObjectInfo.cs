using System.IO;

namespace WindowApplication;

public class ObjectInfo
{
    public string ImagePath { get; set; }
    public string ObjectName { get; set; }
    public string? EditDate { get; set; }
    public string? ObjectSize { get; set; }
    public ObjectType Type { get; init; }

    public ObjectInfo() { }

    public ObjectInfo(FileInfo info)
    {
        ObjectName = info.Name;
        EditDate = info.LastWriteTime.ToString();
        
        var size = info.Length;
        if (size < 1024)
        {
            size = size >> 10;

            if (size < 1024)
            {
                size = size >> 10;

                if (size < 1024)
                {
                    size = size >> 10;

                    ObjectSize = $"{size} ГБ";
                }
                else
                {
                    ObjectSize = $"{size} МБ";
                }
            }
            else
            {
                ObjectSize = $"{size} КБ";
            }
        }
        else
        {
            ObjectSize = $"{size} Б";
        }

        switch (info.Extension)
        {
            case ".mp3":
                ImagePath = @"/Icons/audio_file.png";
                break;
            case ".png":
                ImagePath = @"/Icons/image_file.png";
                break;
            case ".txt":
                ImagePath = @"/Icons/text_file.png";
                break;
            default:
                ImagePath = @"/Icons/default_file.png";
                break;
        }

        Type = ObjectType.File;
    }

    public ObjectInfo(DirectoryInfo info)
    {
        ObjectName = info.Name;
        ImagePath = @"/Icons/folder.png";
        Type = ObjectType.Directory;
    }
}

public enum ObjectType
{
    Directory,
    File
}
