using System.Windows;

namespace CourseProject.UI.Services;

public interface INavigationService
{
    void Navigate(Window window, NavigationMode mode);

    void Back();
}

public enum NavigationMode
{
    Open,
    Set,
    Modal
}
