using System.IO;

namespace WindowApplication;

public class DirectoryManagerInfo : ObjectManagerInfo
{
    public DirectoryManagerInfo(DirectoryInfo info) : base(info.Name, info.FullName, @"/Icons/folder.png") { }
}
