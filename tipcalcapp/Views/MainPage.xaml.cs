using System;
using System.Linq;

using tipcalc;
using tipcalcapp.ViewModels;

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
                    item.TargetType = typeof(CalculatorPage);
                    page = (Page)Activator.CreateInstance(item.TargetType);                    
                    break;

                case 2:
                    item.TargetType = typeof(TipHistoryPage);
                    page = (Page)Activator.CreateInstance(item.TargetType);
                    break;

                case 3:
                    item.TargetType = typeof(LoginPage);
                    page = (Page)Activator.CreateInstance(item.TargetType);
                    break;

                case 4:
                    item.TargetType = typeof(LogoutPage);
                    page = (Page)Activator.CreateInstance(item.TargetType);
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