using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Windows.Shapes;
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
            DataContext = new CounterViewModel(new CounterRepository());
        }
    }
}