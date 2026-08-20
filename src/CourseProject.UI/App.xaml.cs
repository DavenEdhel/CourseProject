using System.Configuration;
using System.Data;
using System.Windows;
using CourseProject.UI.Services;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static INavigationService Navigation { get; } = new NavigationService();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Navigation.Navigate<LoginWindow>(NavigationMode.Set);
    }
}
