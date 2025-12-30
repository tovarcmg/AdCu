using Plugin.Maui.Biometric;

namespace MyMauiApp;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    private async void btnBio_Clicked(object sender, EventArgs e)
    {
        var result = await BiometricAuthenticationService.Default.AuthenticateAsync(

        new AuthenticationRequest()
        {
            Title = "Biometric Request",
            NegativeText = "Cancel authentication"
        },
            CancellationToken.None
        );

        //// The following code block goes here
        ///
        if (result.Status == BiometricResponseStatus.Success)
        {
            Navigation.PushAsync(new MainPage());

        }
        else
        {
            await DisplayAlert("Error", "Authentication failed.", "Ok");
        }



    }
}