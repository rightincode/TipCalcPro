using System;
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
                    CalcTip();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTxt"));
                }
            }
        }

        public string TipTxt
        {
            get { return _calculator.Tip.ToString("F2"); }
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
                    CalcTipPercentage();
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
                CalcTip();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            }
        }

        public string GrandTotalTxt
        {
            get { return _calculator.GrandTotal.ToString("F2"); }
        }

        public void CalcTip()
        {
            _calculator.CalcTip();
            tipTxt = _calculator.Tip.ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }

        public void CalcTipPercentage()
        {
            _calculator.CalcTipPercentage();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }
    }
}
