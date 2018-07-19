using System.Collections.ObjectModel;
using tipcalcapp.Messages;
using tipcalcapp.Validations;
using Xamarin.Forms;

namespace tipcalcapp.ViewModels
{
    public class MainPageMasterViewModel : ExtendedBindableObject
    {
        public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

        public int TipHistoryCount { get; set; }

        public MainPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
            {
                    new MainPageMenuItem { Id = 0, Title = "Home", Image = "baseline_home_black_18dp.png", IsEnabled = true },
                    new MainPageMenuItem { Id = 1, Title = "Tip Calculator", Image = "baseline_payment_black_18dp.png", IsEnabled = true },
                    new MainPageMenuItem { Id = 2, Title = "Tip History", Image = "baseline_list_black_18dp.png", IsEnabled = true },
                    //new MainPageMenuItem { Id = 3, Title = "Login", Image = "baseline_globe_black_18dp.png", IsEnabled = false },
            });

            SubscribeToMessages();
        }
        
        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<CalculatorPageViewModel, int>(
                 this, MessageKeys.SaveTip, (sender, arg) =>
                 {
                     TipHistoryCount = arg;
                     RaisePropertyChanged(() => TipHistoryCount);
                 });
        }
    }
}
