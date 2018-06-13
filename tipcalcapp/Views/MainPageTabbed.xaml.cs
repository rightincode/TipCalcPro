using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tipcalcapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageTabbed : TabbedPage
    {
        public MainPageTabbed ()
        {
            InitializeComponent();

            Children.Add(new HomeDetailPage());
            Children.Add(new CalculatorPage());
            Children.Add(new TipHistoryPage());

            this.CurrentPageChanged += PageChanged;
        }

        public void PageChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;

            var currentPageType = tabbedPage.CurrentPage.GetType();

            if (currentPageType == typeof(TipHistoryPage))
            {
                var tipHistoryPage = (TipHistoryPage)tabbedPage.CurrentPage;
                tipHistoryPage.VM.LoadTipHistory();
            }
        }
    }
}