using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tipcalc_standard.ViewModels;
using Xamarin.Forms;

namespace tipcalc
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;

        public MainPageViewModel VM
        {
            get { return vm; }
        }

        public MainPage()
        {
            InitializeComponent();
            vm = new MainPageViewModel();
            BindingContext = VM;
        }

        public void onTipPercentageValueChanged(Object control, ValueChangedEventArgs args)
        {
            VM.CalcTip(args.NewValue);
        }
    }
}
