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
            InitializeEventHandlers();
            BindingContext = VM;
        }

        private void InitializeEventHandlers()
        {
            tipPercentPreset.SelectedIndexChanged += OnTipPercentPresetSelectedIndexChanged;
        }

        private void OnTipPercentPresetSelectedIndexChanged(Object sender, EventArgs e)
        {
            var TipPercentPresetPicker = (Picker)sender;
            var SelectedTipPercentPreset = (TipPercentage)TipPercentPresetPicker.SelectedItem;

            if (SelectedTipPercentPreset != null)
            {
                VM.TipPercent = SelectedTipPercentPreset.TipPercentageValue;
            }
        }
    }
}
