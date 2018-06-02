using System;

using tipcalcapp.ViewModels;
using tipcalc_core.Interfaces;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tipcalcapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            Page page;

            //Todo: set when constructed?
            switch (item.Id)
            {
                case 0:
                    item.TargetType = typeof(HomeDetailPage);
                    page = (Page)Activator.CreateInstance(item.TargetType);
                    break;

                case 1:
                    page = new CalculatorPage();
                    break;

                case 2:
                    page = new TipHistoryPage();
                    break;

                default:
                    item.TargetType = typeof(HomeDetailPage);
                    page = (Page)Activator.CreateInstance(item.TargetType);
                    break;
            }
                        
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}