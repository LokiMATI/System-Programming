using System.Windows;
using System.Windows.Controls;
namespace WindowApplication;

public class ManagerDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate FileTemplate { get; set; }
    public DataTemplate DirectoryTemplate { get; set; }
    public DataTemplate DriveTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        FrameworkElement element = container as FrameworkElement;

        if (element != null && item != null)
        {
            if (item is FileManagerInfo)
                return FileTemplate;
            if (item is DirectoryManagerInfo)
                return DirectoryTemplate;
            if (item is DriveManagerInfo)
                return DriveTemplate;
        }
        return null;
    }
}
