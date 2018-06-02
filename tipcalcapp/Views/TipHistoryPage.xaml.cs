using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tipcalcapp.ViewModels;
using tipcalc_data.Interfaces;

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
            VM = new TipHistoryPageViewModel(_tipDatabase);
            BindingContext = VM;
            VM.LoadTipHistory();
        }
	}
}