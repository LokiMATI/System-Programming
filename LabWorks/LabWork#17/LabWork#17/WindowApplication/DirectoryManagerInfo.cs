using System.IO;

namespace WindowApplication;

public class DirectoryManagerInfo : ObjectManagerInfo
{
    public DirectoryManagerInfo(DirectoryInfo info) : base(info.Name, @"/Icons/folder.png") { }
}
