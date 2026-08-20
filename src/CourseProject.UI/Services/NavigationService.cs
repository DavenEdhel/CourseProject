using System.Windows;

namespace CourseProject.UI.Services;

public class NavigationService : INavigationService
{
    private Window? activeWindow;

    public void Navigate<T>(NavigationMode mode) where T : Window, new()
    {
        var window = new T();

        switch (mode)
        {
            case NavigationMode.Open:
            {
                window.Show();
                activeWindow = window;
                break;
            }

            case NavigationMode.Set:
            {
                var previousWindow = activeWindow;
                activeWindow = window;

                // Show the new window before closing the old one - WPF's
                // default ShutdownMode is OnLastWindowClose, so closing the
                // last window first would shut the app down before the new
                // one ever appears.
                window.Show();
                Application.Current.MainWindow = window;

                previousWindow?.Close();
                break;
            }
        }
    }
}
