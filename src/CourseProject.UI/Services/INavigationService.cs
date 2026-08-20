using System.Windows;

namespace CourseProject.UI.Services;

public interface INavigationService
{
    void Navigate<T>(NavigationMode mode) where T : Window, new();
}

public enum NavigationMode
{
    Open,
    Set
}
