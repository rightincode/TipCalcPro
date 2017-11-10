using System;
using tipcalc_core.Interfaces;

namespace tipcalc_core.Models
{
    public class TipCalculator : ITipCalculator
    {
        private double total;
        private double tip;
        private double tipPercent;
        private double grandTotal;

        public double Total {
            get { return total; }
            set { total = value; }
        }

        public double Tip
        {
            get { return tip; }
            set { tip = value; }
        }

        public double TipPercent
        {
            get { return tipPercent; }
            set { tipPercent = Math.Round(value); }
        }

        public double GrandTotal
        {
            get { return grandTotal; }
        }

        public void CalcTip()
        {
            if (tipPercent > 0)
            {
                tip = Math.Round(total * (tipPercent / 100), 2);                
            }
            else
            {
                tip = 0;
            }
            UpdateGrandTotal();
        }

        public void CalcTipPercentage()
        {
            if (total > 0)
            {
                tipPercent = Math.Round((tip / total) * 100, 1);
            }
            else
            {
                tipPercent = 0;
            }

            UpdateGrandTotal();
        }
        
        private void UpdateGrandTotal()
        {
            grandTotal = total + tip;
        }
    }
}
