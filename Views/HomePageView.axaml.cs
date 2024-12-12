using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Views;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();
        DataContext = new HomePageViewModel();
    }
}