using Microsoft.Identity.Client;
using System;
using tipcalc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tipcalcapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            try
            {
                bool authenticated = await App.AuthenticationProvider.LoginAsync(true);
                if (authenticated)
                {
                    Application.Current.MainPage = new SharedTipHistoryPage();
                }
                else
                {
                    try
                    {
                        authenticated = await App.AuthenticationProvider.LoginAsync();
                        if (authenticated)
                        {
                            Application.Current.MainPage = new SharedTipHistoryPage();
                        }
                        else
                        {
                            await DisplayAlert("Authentication", "Authentication", "OK");
                        }
                    }
                    catch (MsalException ex)
                    {
                        if (ex.ErrorCode == "authentication_canceled")
                        {
                            await DisplayAlert("Authentication", "Authentication was cancelled by the user.", "OK");
                        }
                        else
                        {
                            await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Authentication", "Authentication failed. Exception: " + ex.Message, "OK");
                    }
                }
            }
            catch
            {
                // Do nothing - the user isn't logged in
            }

            base.OnAppearing();
        }
    }   
}