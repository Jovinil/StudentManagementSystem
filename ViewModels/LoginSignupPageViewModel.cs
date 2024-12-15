using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

namespace StudentManagementSystem.ViewModels
{
	partial class LoginSignupPageViewModel : ViewModelBase
	{

		[ObservableProperty]
		private string _runText = "Don't have an account? ";

		[ObservableProperty]
		private string _navText = "Sign up";

        [ObservableProperty]
		private ViewModelBase _currentPage = new LoginPageViewModel();

		[RelayCommand]
        private void ChangeLoginSignup()
		{
			if(CurrentPage is LoginPageViewModel)
			{
				CurrentPage = new SignUpPageViewModel();
				RunText = "Already have an account? ";
				NavText = "Log in";
			}else
			{
				CurrentPage = new LoginPageViewModel();
				RunText = "Don't have an account? ";
				NavText = "Sign up";
			}
		}
    }
}