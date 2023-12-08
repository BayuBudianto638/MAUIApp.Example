using MAUIApp.Example.Services.LoginAppService;
using MAUIApp.Example.ViewModels;
using MAUIApp.Example.Views.RegisterViews;

namespace MAUIApp.Example.Views.LoginViews;

public partial class LoginViewPage : ContentPage
{
    public LoginViewPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(new LoginAppService());
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }

    async void SignedUpButton_Clicked(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterViewPage());
    }
}