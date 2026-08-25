using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.Services;

namespace CourseProject.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService navigation;
    private readonly ICatRepository catRepository;

    public ObservableCollection<CardEditViewModel> Cards { get; } = new();

    public MainViewModel(INavigationService navigation, ICatRepository catRepository)
    {
        this.navigation = navigation;
        this.catRepository = catRepository;

        foreach (var cat in catRepository.GetAll())
        {
            Cards.Add(new CardEditViewModel(catRepository, navigation, cat));
        }
    }

    [RelayCommand]
    private void Add()
    {
        var card = new CardEditViewModel(catRepository, navigation);
        navigation.Navigate(new CardEditWindow(card), NavigationMode.Modal);

        if (card.Saved)
        {
            Cards.Add(card);
        }
    }

    [RelayCommand]
    private void Statistics()
    {
        var statisticsViewModel = new StatisticsViewModel(catRepository);
        navigation.Navigate(new StatisticsWindow(statisticsViewModel), NavigationMode.Modal);
    }

    [RelayCommand]
    private void Edit(CardEditViewModel card)
    {
        navigation.Navigate(new CardEditWindow(card), NavigationMode.Modal);
    }

    [RelayCommand]
    private void Delete(CardEditViewModel card)
    {
        catRepository.Delete(card.Id);
        Cards.Remove(card);
    }
}
