using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CourseProject.DataLayer.Models;
using CourseProject.DataLayer.Repositories;

namespace CourseProject.UI.ViewModels;

public sealed class AgeBucket
{
    public string Label { get; init; } = string.Empty;

    public int Count { get; init; }

    public double BarHeight { get; init; }
}

public partial class StatisticsViewModel : ObservableObject
{
    private const double PlotAreaHeight = 120;
    private const double PieRadius = 90;

    public ObservableCollection<AgeBucket> AgeHistogram { get; } = new();

    [ObservableProperty]
    private Geometry maleGeometry = Geometry.Empty;

    [ObservableProperty]
    private Geometry femaleGeometry = Geometry.Empty;

    [ObservableProperty]
    private string maleLabel = string.Empty;

    [ObservableProperty]
    private string femaleLabel = string.Empty;

    public StatisticsViewModel(ICatRepository catRepository)
    {
        var cats = catRepository.GetAll();

        BuildAgeHistogram(cats);
        BuildGenderPie(cats);
    }

    private void BuildAgeHistogram(IReadOnlyList<Cat> cats)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var buckets = cats
            .Where(c => c.DateOfBirth.HasValue)
            .Select(c => GetAgeInYears(c.DateOfBirth!.Value, today))
            .GroupBy(age => age)
            .OrderBy(g => g.Key)
            .Select(g => new { Age = g.Key, Count = g.Count() })
            .ToList();

        var maxCount = buckets.Count == 0 ? 0 : buckets.Max(b => b.Count);

        AgeHistogram.Clear();
        foreach (var bucket in buckets)
        {
            AgeHistogram.Add(new AgeBucket
            {
                Label = $"{bucket.Age}",
                Count = bucket.Count,
                BarHeight = maxCount == 0 ? 0 : bucket.Count / (double)maxCount * PlotAreaHeight
            });
        }
    }

    private void BuildGenderPie(IReadOnlyList<Cat> cats)
    {
        var total = cats.Count;
        var maleCount = cats.Count(c => c.IsMale);
        var femaleCount = total - maleCount;

        MaleLabel = total == 0
            ? "Коты: 0"
            : $"Коты: {maleCount} ({maleCount * 100 / total}%)";
        FemaleLabel = total == 0
            ? "Кошки: 0"
            : $"Кошки: {femaleCount} ({femaleCount * 100 / total}%)";

        if (total == 0)
        {
            MaleGeometry = Geometry.Empty;
            FemaleGeometry = Geometry.Empty;
            return;
        }

        var maleSweep = maleCount / (double)total * 360.0;

        MaleGeometry = CreatePieSlice(0, maleSweep);
        FemaleGeometry = CreatePieSlice(maleSweep, 360);
    }

    private static int GetAgeInYears(DateOnly dateOfBirth, DateOnly today)
    {
        var age = today.Year - dateOfBirth.Year;
        if (today.Month < dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day))
        {
            age--;
        }

        return Math.Max(age, 0);
    }

    private static Geometry CreatePieSlice(double startAngleDegrees, double endAngleDegrees)
    {
        var center = new Point(PieRadius, PieRadius);

        if (endAngleDegrees - startAngleDegrees >= 359.999)
        {
            return new EllipseGeometry(center, PieRadius, PieRadius);
        }

        if (endAngleDegrees - startAngleDegrees <= 0.001)
        {
            return Geometry.Empty;
        }

        var startPoint = GetPointOnCircle(center, startAngleDegrees);
        var endPoint = GetPointOnCircle(center, endAngleDegrees);
        var isLargeArc = endAngleDegrees - startAngleDegrees > 180;

        var figure = new PathFigure { StartPoint = center, IsClosed = true };
        figure.Segments.Add(new LineSegment(startPoint, true));
        figure.Segments.Add(new ArcSegment(endPoint, new Size(PieRadius, PieRadius), 0, isLargeArc, SweepDirection.Clockwise, true));

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);
        return geometry;
    }

    private static Point GetPointOnCircle(Point center, double angleDegrees)
    {
        var angleRadians = (angleDegrees - 90) * Math.PI / 180.0;
        return new Point(
            center.X + PieRadius * Math.Cos(angleRadians),
            center.Y + PieRadius * Math.Sin(angleRadians));
    }
}
