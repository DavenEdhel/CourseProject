using System.Windows;
using System.Windows.Input;
using CourseProject.UI.ViewModels;
using Microsoft.Win32;

namespace CourseProject.UI;

/// <summary>
/// Interaction logic for CardEditWindow.xaml
/// </summary>
public partial class CardEditWindow : Window
{
    public CardEditWindow(CardEditViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is not CardEditViewModel viewModel)
        {
            return;
        }

        var dialog = new OpenFileDialog
        {
            Title = "Выберите фото",
            Filter = "Изображения (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
        };

        if (dialog.ShowDialog() == true)
        {
            viewModel.SetPhoto(dialog.FileName);
        }
    }
}
