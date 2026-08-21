using System.Windows;
using CourseProject.UI.ViewModels;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for CardEditWindow.xaml
/// </summary>
public partial class CardEditWindow : Window
{
    public CardEditWindow(CardEditViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ButtonSave_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }
}
