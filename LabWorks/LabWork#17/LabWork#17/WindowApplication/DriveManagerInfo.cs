using System.IO;

namespace WindowApplication;

public class DriveManagerInfo : ObjectManagerInfo
{
    public string Size { get; set; }
    public long TotalSize { get; set; }
    public long OccupiedSize { get; set; }
    public DriveManagerInfo(DriveInfo info) : base(info.Name, @"/Icons/hard_drive.png")
    {
        Size = $"Свободно {GetSize(info.AvailableFreeSpace)} / {GetSize(info.TotalSize)}";
        TotalSize = info.TotalSize;
        OccupiedSize = info.TotalSize - info.TotalFreeSpace;
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
