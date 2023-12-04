using MAUIApp.Example.Services.LoginAppService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIApp.Example.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        private readonly ILoginAppService _loginAppService;
        public ICommand LoginCommand { get; }

        public LoginViewModel(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;

            LoginCommand = new Command(async () => await OnLoginButtonClicked());
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> LoginAsync()
        {
            return await _loginAppService.LoginAsync(Username, Password);
        }

        private async Task OnLoginButtonClicked()
        {
            bool isCredentialCorrect = await LoginAsync();

            if (!isCredentialCorrect)
            {
                await SecureStorage.SetAsync("hasAuth", "true");
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login failed", "Username or password is invalid", "Try again");
            }
        }
    }
}
