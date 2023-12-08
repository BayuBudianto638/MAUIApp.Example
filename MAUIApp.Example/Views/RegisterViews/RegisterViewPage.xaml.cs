using MAUIApp.Example.Services.RegisterAppService;
using MAUIApp.Example.Views.LoginViews;

namespace MAUIApp.Example.Views.RegisterViews;

public partial class RegisterViewPage : ContentPage
{
	public RegisterViewPage()
	{
		InitializeComponent();
	}

    async void BtnRegister_Clicked(System.Object sender, System.EventArgs e)
    {
        var response = await RegisterAppService.RegisterUser(EntFullName.Text, EntEmail.Text, EntPassword.Text);
        if (response)
        {
            await DisplayAlert("", "Your account has been created", "Alright");
            await Navigation.PushModalAsync(new LoginViewPage());
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }

    async void SignIn_Clicked(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new LoginViewPage());
    }
}