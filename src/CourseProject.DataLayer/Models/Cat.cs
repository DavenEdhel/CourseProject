using System;
using System.Collections.Generic;

namespace CourseProject.DataLayer.Models;

public partial class Cat
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public string Color { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public double Weight { get; set; }

    public string EyeColor { get; set; } = null!;

    public bool IsMale { get; set; }

    public bool IsVaccinated { get; set; }

    public bool IsSterilized { get; set; }

    public bool HasDocuments { get; set; }

    public string? PhotoPath { get; set; }
}
