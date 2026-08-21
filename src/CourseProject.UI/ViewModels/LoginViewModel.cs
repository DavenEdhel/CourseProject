using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.DataLayer.BusinessModels;
using CourseProject.DataLayer.Models;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.Services;

namespace CourseProject.UI.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IUserRepository userRepository;
    private readonly INavigationService navigation;

    [ObservableProperty]
    private string userName = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    private static bool bypassAuth = true;

    public LoginViewModel(IUserRepository userRepository, INavigationService navigation)
    {
        this.userRepository = userRepository;
        this.navigation = navigation;
    }

    [RelayCommand]
    private void Login()
    {
        if (bypassAuth)
        {
            UserName = "Наська";
            Password = "Рыжик";
        }

        if (string.IsNullOrWhiteSpace(UserName))
        {
            ErrorMessage = "Укажите имя";

            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Укажите пароль";

            return;
        }

        var user = userRepository.TryGet(UserName, Password);

        if (user is null)
        {
            ErrorMessage = "Неверное имя пользователя или пароль.";
        }
        else
        {
            CurrentUser.Instance.Set(user);
            navigation.Navigate(new MainWindow(), NavigationMode.Set);
        }
    }
}
