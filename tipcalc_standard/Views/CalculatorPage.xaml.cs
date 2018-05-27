using System;
using tipcalc_standard.ViewModels;
using tipcalc_core.Interfaces;
using Xamarin.Forms;

namespace tipcalc_standard.Views
{
    public partial class CalculatorPage : ContentPage
    {
        private MainPageViewModel vm;

        public MainPageViewModel VM
        {
            get { return vm; }
        }

        public CalculatorPage(ITipCalculator tipCalculator)
        {
            InitializeComponent();
            vm = new MainPageViewModel(tipCalculator);
            BindingContext = VM;
        }
    }
}
