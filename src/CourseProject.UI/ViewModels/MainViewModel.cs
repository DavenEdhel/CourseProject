using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.UI.Services;

namespace CourseProject.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService navigation;

    public ObservableCollection<CardEditViewModel> Cards { get; } = new()
    {
        new CardEditViewModel()
        {
            Breed = "aaa",
            Color = "red",
            DateOfBirth = DateTime.Now.AddMonths(-6),
            EyeColor = "green",
            HasDocuments = true,
            IsFemale = false,
            IsMale = true,
            IsSterilized = true,
            IsVaccinated = true,
            Name = "Redgy",
            PhotoPath = "/Images/bengal.jpg",
            Weight = 5
        }
    };

    public MainViewModel(INavigationService navigation)
    {
        this.navigation = navigation;
    }

    [RelayCommand]
    private void Add()
    {
        var card = new CardEditViewModel();
        navigation.Navigate(new CardEditWindow(card), NavigationMode.Modal);

        if (card.Saved)
        {
            Cards.Add(card);
        }
    }

    [RelayCommand]
    private void Edit(CardEditViewModel card)
    {
        navigation.Navigate(new CardEditWindow(card), NavigationMode.Modal);
    }

    [RelayCommand]
    private void Delete(CardEditViewModel card)
    {
        Cards.Remove(card);
    }
}
