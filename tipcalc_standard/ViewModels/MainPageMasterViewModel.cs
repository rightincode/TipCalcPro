using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace tipcalcapp.ViewModels
{
    public class MainPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

        public MainPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
            {
                    new MainPageMenuItem { Id = 0, Title = "Home", Image = "baseline_home_black_18dp.png", IsEnabled = true },
                    new MainPageMenuItem { Id = 1, Title = "Tip Calculator", Image = "baseline_payment_black_18dp.png", IsEnabled = true },
                    new MainPageMenuItem { Id = 2, Title = "Login", Image = "baseline_globe_black_18dp.png", IsEnabled = false },
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
