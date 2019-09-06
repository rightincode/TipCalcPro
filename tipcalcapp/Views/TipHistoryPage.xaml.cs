using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

using tipcalcapp.ViewModels;
using tipcalc_data.Interfaces;
using Application = Xamarin.Forms.Application;

namespace tipcalcapp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TipHistoryPage : ContentPage
	{
        private readonly ITipDatabase _tipDatabase = ((tipcalc.App)Application.Current).ServiceProvider.GetService<ITipDatabase>();

        public TipHistoryPageViewModel VM { get; }

        public TipHistoryPage ()
		{
			InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            VM = new TipHistoryPageViewModel(_tipDatabase);
            BindingContext = VM;
            VM.LoadTipHistory();
        }
	}
}