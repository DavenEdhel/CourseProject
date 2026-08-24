using System.ComponentModel;
using System.Windows;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.ViewModels;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // The WPF XAML designer instantiates this window (and runs this
        // constructor) to render the preview while MainWindow.xaml is open
        // in the editor. Skip touching the database in that case.
        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            DataContext = new MainViewModel(App.Navigation, new CatRepository());
        }
    }
}
