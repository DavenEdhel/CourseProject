using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CourseProject.UI.Converters;

public sealed class PhotoPathToImageSourceConverter : IValueConverter
{
    private static readonly Uri FallbackUri = new("pack://application:,,,/CourseProject.UI;component/Images/no_img.png", UriKind.Absolute);

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string relativePath && !string.IsNullOrWhiteSpace(relativePath))
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);
            if (File.Exists(fullPath))
            {
                return new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            }
        }

        return new BitmapImage(FallbackUri);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
