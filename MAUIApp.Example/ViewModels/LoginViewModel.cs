using MAUIApp.Example.Models;
using MAUIApp.Example.Services.LoginAppService;
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
    public class LoginViewModel: INotifyPropertyChanged
    {
        private readonly LoginAppService _loginAppService;
        public ICommand LoginCommand { get; }

        public LoginViewModel(LoginAppService loginAppService)
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

        private string setApiUrl()
        {
            string jsonConfig = LoadJsonConfig("appconfig.json");

            if (!string.IsNullOrEmpty(jsonConfig))
            {
                AppConfig appConfig = JsonSerializer.Deserialize<AppConfig>(jsonConfig);
                ApiUrl = appConfig.RestApiUrl;
            }

            return ApiUrl;
        }

        private static string? LoadJsonConfig(string fileName)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON configuration: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> LoginAsync()
        {
            _loginAppService.SetApiUrl(setApiUrl());
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
