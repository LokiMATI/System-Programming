namespace WindowApplication;

abstract public class ObjectManagerInfo
{
    public string Name { get; set; }
    public string FullName { get; set; }
    public string ImagePath { get; set; }

    public ObjectManagerInfo(string name, string fullName)
    {
        Name = name;
        FullName = fullName;
    }

    public ObjectManagerInfo(string name, string fullName, string imagePath)
    {
        Name = name;
        FullName = fullName;
        ImagePath = imagePath;
    }
}
