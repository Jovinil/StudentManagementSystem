using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using StudentManagementSystem.Views;
using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StudentManagementSystem.ViewModels
{
    public partial class ClassPageViewModel : ViewModelBase
    {
        [RelayCommand]
        private void OpenModal()
        {
            var window = new ModalView();
            window.Show();
        }
    }
}