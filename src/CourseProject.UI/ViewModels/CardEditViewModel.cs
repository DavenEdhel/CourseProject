using System;
using System.Collections.Generic;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.DataLayer.Models;
using CourseProject.DataLayer.Repositories;
using CourseProject.UI.Services;

namespace CourseProject.UI.ViewModels;

public partial class CardEditViewModel : ObservableObject
{
    private const string AvatarsFolderName = "avatars";

    private readonly ICatRepository catRepository;
    private readonly INavigationService navigationService;

    public static IReadOnlyList<string> EyeColorOptions { get; } = new[] { "Зеленые", "Голубые", "Желтые", "Карие" };

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
    private void SetMale()
    {
        IsMale = true;
    }

    [RelayCommand]
    private void SetFemale()
    {
        IsMale = false;
    }

    public void SetPhoto(string sourceFilePath)
    {
        var avatarsDirectory = Path.Combine(AppContext.BaseDirectory, AvatarsFolderName);
        Directory.CreateDirectory(avatarsDirectory);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(sourceFilePath)}";
        var destinationPath = Path.Combine(avatarsDirectory, fileName);

        File.Copy(sourceFilePath, destinationPath, overwrite: true);

        PhotoPath = Path.Combine(AvatarsFolderName, fileName);
    }

    public Cat ToCat()
    {
        return new Cat
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
    }

    [RelayCommand]
    private void Save()
    {
        var cat = ToCat();

        catRepository.Save(cat);

        Id = cat.Id;
        Saved = true;

        navigationService.Back();
    }
}
