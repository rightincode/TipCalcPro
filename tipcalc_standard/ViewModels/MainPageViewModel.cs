using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using tipclac_core;

namespace tipcalc_standard.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string totalTxt;
        private TipCalculator _calculator;

        public MainPageViewModel()
        {
            _calculator = new TipCalculator();
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
