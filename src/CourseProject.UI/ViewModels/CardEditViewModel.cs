using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CourseProject.UI.ViewModels;

public partial class CardEditViewModel : ObservableObject
{
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
    private bool isFemale;

    [ObservableProperty]
    private bool isVaccinated;

    [ObservableProperty]
    private bool isSterilized;

    [ObservableProperty]
    private bool hasDocuments;

    public bool Saved { get; set; } = false;
}
