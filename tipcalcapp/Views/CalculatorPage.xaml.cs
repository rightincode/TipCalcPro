using System;
using tipcalcapp.ViewModels;
using tipcalc_core.Interfaces;
using tipcalc_data.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace tipcalcapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        private readonly ITipCalculator _tipCalculator = ((tipcalc.App)Application.Current).ServiceProvider.GetService<ITipCalculator>();
        private readonly ITipCalcTransaction _tipCalcTransaction = ((tipcalc.App)Application.Current).ServiceProvider.GetService<ITipCalcTransaction>();
        private readonly ITipDatabase _tipDatabase = ((tipcalc.App)Application.Current).ServiceProvider.GetService<ITipDatabase>();

        public CalculatorPageViewModel VM { get; }

        public CalculatorPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            VM = new CalculatorPageViewModel(_tipCalculator, _tipCalcTransaction, _tipDatabase);
            InitializeEventHandlers();
            BindingContext = VM;
        }

        private void InitializeEventHandlers()
        {
            tipPercentPreset.SelectedIndexChanged += OnTipPercentPresetSelectedIndexChanged;
            swtRounded.Toggled += OnSwtRoundedToggled;
            sldTipCalc.ValueChanged += OnSldTipCalcValueChanged;
            stpNumberOfPersons.ValueChanged += OnStpNumberOfPersonsValueChanged;
        }

        private void OnTipPercentPresetSelectedIndexChanged(Object sender, EventArgs e)
        {
            var TipPercentPresetPicker = (Xamarin.Forms.Picker)sender;
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

        private void OnSwtRoundedToggled(object sender, EventArgs e)
        {           
            var roundedSwitch = (Switch)sender;

            if (roundedSwitch != null)
            {
                if (roundedSwitch.IsToggled)
                {
                    VM.RoundTotal();
                }
                else
                {
                    VM.UnRoundTotal();
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

        private void OnStpNumberOfPersonsValueChanged(object sender, ValueChangedEventArgs e)
        {
        }

    }
}
