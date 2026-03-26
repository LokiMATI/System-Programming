namespace WindowApplication;

abstract public class ObjectManagerInfo
{
    public string Name { get; set; }
    public string ImagePath { get; set; }

    public ObjectManagerInfo(string name)
    {
        Name = name;
    }

    public ObjectManagerInfo(string name, string imagePath)
    {
        Name = name;
        ImagePath = imagePath;
    }
}
