using System;
using System.ComponentModel;
using tipcalc_core.Interfaces;

namespace tipcalc_standard.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string totalTxt;
        private double tipPercent;
        private ITipCalculator _calculator;

        public MainPageViewModel(ITipCalculator tipCalculator)
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
                    _calculator.Total = double.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Total = 0;
                }
                finally
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTxt"));
                }
            }
        }

        public string TipTxt
        {
            get { return _calculator.Tip.ToString(); }
        }

        public double TipPercent
        {
            get { return tipPercent; }
            set {
                tipPercent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
            }
        }

        public string GrandTotalTxt
        {
            get { return _calculator.GrandTotal.ToString(); }
        }

        public void CalcTip(double tipPercent)
        {
            _calculator.CalcTip(tipPercent);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }
    }
}
