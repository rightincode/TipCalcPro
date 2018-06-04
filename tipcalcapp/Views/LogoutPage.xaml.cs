using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tipcalc;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tipcalcapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoutPage : ContentPage
	{
		public LogoutPage ()
		{
			InitializeComponent ();

            App.AuthenticationProvider.Logout();
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
	}
}