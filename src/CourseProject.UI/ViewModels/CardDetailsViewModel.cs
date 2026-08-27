using System;
using CourseProject.DataLayer.Models;

namespace CourseProject.UI.ViewModels;

public sealed class CardDetailsViewModel
{
    public string? PhotoPath { get; }

    public string Name { get; }

    public string Breed { get; }

    public string Color { get; }

    public DateTime? DateOfBirth { get; }

    public double Weight { get; }

    public string EyeColor { get; }

    public bool IsMale { get; }

    public string GenderText => IsMale ? "Кот" : "Кошка";

    public bool IsVaccinated { get; }

    public bool IsSterilized { get; }

    public bool HasDocuments { get; }

    public CardDetailsViewModel(Cat cat)
    {
        PhotoPath = cat.PhotoPath;
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
    }
}
