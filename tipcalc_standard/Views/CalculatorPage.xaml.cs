using System;
using tipcalc_standard.ViewModels;
using tipcalc_core.Interfaces;
using Xamarin.Forms;

namespace tipcalc_standard.Views
{
    public partial class CalculatorPage : ContentPage
    {

        public CalculatorPageViewModel VM { get; }

        public CalculatorPage(ITipCalculator tipCalculator)
        {
            InitializeComponent();
            VM = new CalculatorPageViewModel(tipCalculator);
            BindingContext = VM;
        }
    }
}
