using System.ComponentModel;
using System.Windows;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.ViewModels;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();

        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            DataContext = new LoginViewModel(new UserRepository(), App.Navigation);
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel viewModel)
        {
            viewModel.Password = PasswordBox.Password;
        }
    }
}
