using System.IO;

namespace WindowApplication;

public class FileManagerInfo : ObjectManagerInfo
{
    public string EditDate { get; set; }
    public string Size { get; set; }

    public FileManagerInfo(FileInfo info) : base(info.Name)
    {
        EditDate = info.LastWriteTime.ToString();
        Size = GetSize(info.Length);

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
    }

    private string GetSize(long size)
    {
        if (size > 1024)
        {
            size = size >> 10;

            if (size > 1024)
            {
                size = size >> 10;

                if (size > 1024)
                {
                    size = size >> 10;

                    return $"{size} ГБ";
                }
                else
                {
                    return $"{size} МБ";
                }
            }
            else
            {
                return $"{size} КБ";
            }
        }
        return $"{size} Б";
    }
}
