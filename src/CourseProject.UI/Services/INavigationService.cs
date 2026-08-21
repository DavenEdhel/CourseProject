using System.Windows;

namespace CourseProject.UI.Services;

public interface INavigationService
{
    void Navigate(Window window, NavigationMode mode);
}

public enum NavigationMode
{
    Open,
    Set,
    Modal
}
