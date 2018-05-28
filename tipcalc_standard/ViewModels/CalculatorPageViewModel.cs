using System;
using System.Collections.Generic;
using System.ComponentModel;
using tipcalc_core.Interfaces;

namespace tipcalc_standard.ViewModels
{
    public class CalculatorPageViewModel : INotifyPropertyChanged
    {
        private string totalTxt;
        private string tipTxt;
        private readonly ITipCalculator _calculator;

        public CalculatorPageViewModel(ITipCalculator tipCalculator)
        {
            _calculator = tipCalculator;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<TipPercentage> TipPresets { get; } = new List<TipPercentage>
        {
            new TipPercentage{ TipPercentageTxt = "15%", TipPercentageValue = 15},
            new TipPercentage{ TipPercentageTxt = "18%", TipPercentageValue = 18},
            new TipPercentage{ TipPercentageTxt = "20%", TipPercentageValue = 20},
            new TipPercentage{ TipPercentageTxt = "22%", TipPercentageValue = 22},
        };

        public string TotalTxt
        {
            get
            {
                return totalTxt;
            }

            set
            {
                totalTxt = value;

                try
                {
                    string newValue = value;
                    _calculator.Total = decimal.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Total = 0;
                }
                finally
                {
                    _calculator.CalcTip();
                    CalculateTipPropertyChangedNotifications();
                }
            }
        }

        public string TipTxt
        {
            get { return _calculator.Tip.ToString(); }
            set
            {
                tipTxt = value;

                try
                {
                    string newValue = value;
                    _calculator.Tip = decimal.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Tip = 0;
                }
                finally
                {
                    _calculator.CalcTipPercentage();
                    CalculateTipPercentagePropertyChangedNotifications();
                }
            }
        }
        
        public string TipPercentTxt
        {
            get { return _calculator.TipPercent.ToString(); }
        }

        public decimal TipPercent
        {
            get { return _calculator.TipPercent; }
            set {
                _calculator.TipPercent = value;
                _calculator.CalcTip();
                CalculateTipPropertyChangedNotifications();
            }
        }

        public string GrandTotalTxt
        {
            get { return _calculator.GrandTotal.ToString("F2"); }
        }

        private void CalculateTipPropertyChangedNotifications()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }

        private void CalculateTipPercentagePropertyChangedNotifications()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }


    }
}
