using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using StudentManagementSystem.Messages;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using System;

namespace StudentManagementSystem.ViewModels
{
    public partial class SignUpPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _firstName;

        [ObservableProperty]
        private string? _middleName;

        [ObservableProperty]
        private string? _lastName;

        [ObservableProperty]
        private string? _username;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private string? _error;

        private readonly ISignupService _signupService;
        private readonly IMessenger _messenger;

        public SignUpPageViewModel()
        {
            _signupService= new SignupService();
            _messenger = WeakReferenceMessenger.Default;
        }

        [RelayCommand]
        public void SignupBtnClicked()
        {


            if (FirstName is null ||
                LastName is null ||
                Username is null ||
                Password is null)
            {
                _error = "Empty Fields Found";
            }
            else
            {
                User user = new User();
                user.FirstName = FirstName;
                user.MiddleName = MiddleName;
                user.LastName = LastName;
                user.Username = Username;
                user.Password = Password;
                var result = _signupService.Signup(user);

                if (result) _messenger.Send(new SignupSuccessMessage(result));

                _error = "Registration Failed";
            }



        }
    }
}