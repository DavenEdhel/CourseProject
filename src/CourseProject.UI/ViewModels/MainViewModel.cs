using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.Services;

namespace CourseProject.UI.ViewModels;

public enum CatGenderFilter
{
    All,
    Boys,
    Girls
}

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService navigation;
    private readonly ICatRepository catRepository;

    public ObservableCollection<CardEditViewModel> Cards { get; } = new();

    public ICollectionView CardsView { get; }

    [ObservableProperty]
    private string nameFilter = string.Empty;

    [ObservableProperty]
    private CatGenderFilter genderFilter = CatGenderFilter.All;

    [ObservableProperty]
    private int? minAge;

    [ObservableProperty]
    private int? maxAge;

    public MainViewModel(INavigationService navigation, ICatRepository catRepository)
    {
        this.navigation = navigation;
        this.catRepository = catRepository;

        foreach (var cat in catRepository.GetAll())
        {
            Cards.Add(new CardEditViewModel(catRepository, navigation, cat));
        }

        CardsView = CollectionViewSource.GetDefaultView(Cards);
        CardsView.Filter = FilterCard;
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

    [RelayCommand]
    private void ViewDetails(CardEditViewModel card)
    {
        var detailsViewModel = new CardDetailsViewModel(card.ToCat());
        navigation.Navigate(new CardDetailsWindow(detailsViewModel), NavigationMode.Modal);
    }

    [RelayCommand]
    private void SetGenderFilter(string filter)
    {
        GenderFilter = Enum.Parse<CatGenderFilter>(filter);
    }

    partial void OnNameFilterChanged(string value) => CardsView.Refresh();

    partial void OnGenderFilterChanged(CatGenderFilter value) => CardsView.Refresh();

    partial void OnMinAgeChanged(int? value) => CardsView.Refresh();

    partial void OnMaxAgeChanged(int? value) => CardsView.Refresh();

    private bool FilterCard(object item)
    {
        if (item is not CardEditViewModel card)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(NameFilter) &&
            card.Name.IndexOf(NameFilter, StringComparison.OrdinalIgnoreCase) < 0)
        {
            return false;
        }

        if (GenderFilter == CatGenderFilter.Boys && !card.IsMale)
        {
            return false;
        }

        if (GenderFilter == CatGenderFilter.Girls && card.IsMale)
        {
            return false;
        }

        if (MinAge.HasValue || MaxAge.HasValue)
        {
            if (card.DateOfBirth is null)
            {
                return false;
            }

            var age = GetAgeInYears(card.DateOfBirth.Value);

            if (MinAge.HasValue && age < MinAge.Value)
            {
                return false;
            }

            if (MaxAge.HasValue && age > MaxAge.Value)
            {
                return false;
            }
        }

        return true;
    }

    private static int GetAgeInYears(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (today.Month < dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day))
        {
            age--;
        }

        return Math.Max(age, 0);
    }
}
