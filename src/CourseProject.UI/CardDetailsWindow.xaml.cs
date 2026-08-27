using System.Windows;
using CourseProject.UI.ViewModels;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for CardDetailsWindow.xaml
/// </summary>
public partial class CardDetailsWindow : Window
{
    public CardDetailsWindow(CardDetailsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ButtonClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
