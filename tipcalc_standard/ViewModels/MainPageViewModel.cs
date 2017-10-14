using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace tipcalc_standard.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private double total;
        private string totalTxt;

        private double tip;

        private double grandTotal;

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
                    total = double.Parse(newValue);
                }
                catch (Exception)
                {
                    total = 0;
                }
                finally
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTxt"));
                }
            }
        }

        public string TipTxt
        {
            get { return tip.ToString(); }
        }

        public string GrandTotalTxt
        {
            get { return grandTotal.ToString(); }
        }

        public void CalcTip(double tipPercent)
        {
            if (tipPercent > 0)
            {
                tip = total * (tipPercent/100);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
                UpdateGrandTotal();
            }            
        }

        private void UpdateGrandTotal()
        {
            grandTotal = total + tip;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
        }
    }
}
