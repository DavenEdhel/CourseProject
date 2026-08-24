using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.DataLayer.Models;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.Services;

namespace CourseProject.UI.ViewModels;

public partial class CardEditViewModel : ObservableObject
{
    private readonly ICatRepository catRepository;
    private readonly INavigationService navigationService;

    public int Id { get; private set; }

    [ObservableProperty]
    private string? photoPath;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string breed = string.Empty;

    [ObservableProperty]
    private string color = string.Empty;

    [ObservableProperty]
    private DateTime? dateOfBirth;

    [ObservableProperty]
    private double weight = 5;

    [ObservableProperty]
    private string eyeColor = string.Empty;

    [ObservableProperty]
    private bool isMale;

    [ObservableProperty]
    private bool isVaccinated;

    [ObservableProperty]
    private bool isSterilized;

    [ObservableProperty]
    private bool hasDocuments;

    public bool Saved { get; set; } = false;

    public CardEditViewModel(ICatRepository catRepository, INavigationService navigationService)
    {
        this.catRepository = catRepository;
        this.navigationService = navigationService;
    }

    public CardEditViewModel(ICatRepository catRepository, INavigationService navigationService, Cat cat)
        : this(catRepository, navigationService)
    {
        Id = cat.Id;
        Name = cat.Name;
        Breed = cat.Breed;
        Color = cat.Color;
        DateOfBirth = cat.DateOfBirth?.ToDateTime(TimeOnly.MinValue);
        Weight = cat.Weight;
        EyeColor = cat.EyeColor;
        IsMale = cat.IsMale;
        IsVaccinated = cat.IsVaccinated;
        IsSterilized = cat.IsSterilized;
        HasDocuments = cat.HasDocuments;
        PhotoPath = cat.PhotoPath;
    }

    [RelayCommand]
    private void Save()
    {
        var cat = new Cat
        {
            Id = Id,
            Name = Name,
            Breed = Breed,
            Color = Color,
            DateOfBirth = DateOfBirth.HasValue ? DateOnly.FromDateTime(DateOfBirth.Value) : null,
            Weight = Weight,
            EyeColor = EyeColor,
            IsMale = IsMale,
            IsVaccinated = IsVaccinated,
            IsSterilized = IsSterilized,
            HasDocuments = HasDocuments,
            PhotoPath = PhotoPath
        };

        catRepository.Save(cat);

        Id = cat.Id;
        Saved = true;

        navigationService.Back();
    }
}
