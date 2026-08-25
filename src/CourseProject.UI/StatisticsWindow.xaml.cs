using System.Windows;
using CourseProject.UI.ViewModels;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for StatisticsWindow.xaml
/// </summary>
public partial class StatisticsWindow : Window
{
    public StatisticsWindow(StatisticsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
