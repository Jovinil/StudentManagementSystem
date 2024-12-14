using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Views;

public partial class ModalView : Window
{
    public ModalView()
    {
        InitializeComponent();
        DataContext = new ModalViewModel();
    }
    
    private void OnCloseButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(); // Closes the modal window
    }
}