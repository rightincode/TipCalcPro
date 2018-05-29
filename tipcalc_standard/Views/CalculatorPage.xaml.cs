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
            btnResetTipCalculator.Clicked += OnBtnResetTipCalculatorClicked;
            swtRounded.Toggled += OnSwtRoundedToggled;
            sldTipCalc.ValueChanged += OnSldTipCalcValueChanged;
        }

        private void OnTipPercentPresetSelectedIndexChanged(Object sender, EventArgs e)
        {
            var TipPercentPresetPicker = (Picker)sender;
            var SelectedTipPercentPreset = (TipPercentage)TipPercentPresetPicker.SelectedItem;

            if (SelectedTipPercentPreset != null)
            {
                VM.TipPercent = SelectedTipPercentPreset.TipPercentageValue;

                if ((swtRounded != null) && (swtRounded.IsToggled))
                {
                    //VM.RoundTip();
                    swtRounded.IsToggled = false;
                }
            }
        }

        private void OnBtnResetTipCalculatorClicked(object sender, EventArgs e)
        {
            tipPercentPreset.SelectedIndex = -1;
            VM.ResetCalculator();
        }

        private void OnSwtRoundedToggled(object sender, EventArgs e)
        {
            var roundedSwitch = (Switch)sender;

            if (roundedSwitch != null)
            {
                if (roundedSwitch.IsToggled)
                {
                    VM.RoundTip();
                }
                else
                {
                    VM.UnRoundTip();
                }
            }
        }

        private void OnSldTipCalcValueChanged(object sender, ValueChangedEventArgs e)
        {
            if ((swtRounded != null) && (swtRounded.IsToggled))
            {
                //VM.RoundTip();
                swtRounded.IsToggled = false;
            }
        }
    }
}
