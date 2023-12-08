using MAUIApp.Example.Helpers;
using MAUIApp.Example.Models;
using MAUIApp.Example.Services.LoginAppService;
using MAUIApp.Example.Services.Sessions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIApp.Example.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginAppService _loginAppService;
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginViewModel(LoginAppService loginAppService)
        {
            _loginAppService = loginAppService;

            LoginCommand = new Command(async () => await OnLoginButtonClicked());
            SignUpCommand = new Command(async () => await OnSignUpButtonClicked());
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string _apiUrl;

        public string ApiUrl
        {
            get => _apiUrl;
            set
            {
                if (_apiUrl != value)
                {
                    _apiUrl = value;
                    OnPropertyChanged(nameof(ApiUrl));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<(bool, string)> LoginAsync()
        {
            return await _loginAppService.LoginAsync(Username, Password);
        }

        private async Task OnLoginButtonClicked()
        {
            var (isCredentialCorrect, Token) = await LoginAsync();

            if (isCredentialCorrect)
            {
                SessionAppService.SetToken(Token);
                // await SecureStorage.SetAsync("hasAuth", "true");
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login failed", "Username or password is invalid", "Try again");
            }
        }

        private async Task OnSignUpButtonClicked()
        {
            await App.Current.MainPage.DisplayAlert("Login failed", "Username or password is invalid", "Try again");
        }
    }
}
