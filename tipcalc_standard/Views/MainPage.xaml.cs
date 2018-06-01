using System;

using tipcalc_standard.ViewModels;
using tipcalc_core.Interfaces;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tipcalc_standard.Views
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
                    //item.TargetType = typeof(CalculatorPage);
                    page = new CalculatorPage();
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