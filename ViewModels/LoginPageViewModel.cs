using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using project_open.Services;
using StudentManagementSystem.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _username = null;

        [ObservableProperty]
        private string? _password = null;

        [ObservableProperty]
        private string? _message;

        [ObservableProperty]
        private int _messageFont = 14;

        [ObservableProperty]
        private IBrush _messageColor = Brushes.Red;

        private readonly ILoginService _loginService;
        private readonly IMessenger _messenger;

        public LoginPageViewModel()
        {
            _loginService = new LoginService();
            _messenger = WeakReferenceMessenger.Default;
        }

        [RelayCommand]
        private async Task LoginBtnClicked()
        {
            MessageFont = 14;
            MessageColor = Brushes.Red;
            if (Username != null && Password != null)
            {
                var result = await _loginService.Authenticate(Username, Password);
                if (result is null)
                {
                    Message = "Wrong username or password";
                }
                else
                {
                    Message = "Login Successful";
                    _messenger.Send(new LoginSuccessMessage(result));
                }
            }
            else
                Message = "Entered empty username or password";
        }
    }
}