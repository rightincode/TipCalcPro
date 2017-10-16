using System;
using tipcalc_standard.ViewModels;
using tipcalc_core.Interfaces;
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

        public MainPage(ITipCalculator tipCalculator)
        {
            InitializeComponent();
            vm = new MainPageViewModel(tipCalculator);
            BindingContext = VM;
        }

        public void onTipPercentageValueChanged(Object control, ValueChangedEventArgs args)
        {
            VM.TipPercent = args.NewValue;
            VM.CalcTip(args.NewValue);
        }
    }
}
