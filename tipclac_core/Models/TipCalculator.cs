using System;
using tipcalc_core.Interfaces;

namespace tipcalc_core.Models
{
    public class TipCalculator : ITipCalculator
    {
        private decimal tipPercent;

        public decimal Total { get; set; }

        public decimal Tip { get; set; }

        public decimal TipPercent
        {
            get { return tipPercent; }
            set { tipPercent = Math.Round(value); }
        }

        public decimal SavedGrandTotal { get; private set; }

        public decimal GrandTotal { get; private set; }

        public void CalcTip()
        {
            if (tipPercent > 0)
            {
                Tip = Math.Round(Total * (tipPercent / 100), 2);                
            }
            else
            {
                Tip = 0;
            }
            UpdateGrandTotal();
        }

        public void CalcTipPercentage()
        {
            if (Total > 0)
            {
                tipPercent = Math.Round((Tip / Total) * 100, 1);
            }
            else
            {
                tipPercent = 0;
            }

            UpdateGrandTotal();
        }

        public void RoundTip()
        {
            SavedGrandTotal = GrandTotal;
            GrandTotal = Math.Round(GrandTotal);
        }

        public void UnRoundTip()
        {
            GrandTotal = SavedGrandTotal;
            SavedGrandTotal = 0;
        }

        public void Reset()
        {
            Total = 0;
            Tip = 0;
            TipPercent = 0;
            GrandTotal = 0;
            SavedGrandTotal = 0;
        }
        
        private void UpdateGrandTotal()
        {
            SavedGrandTotal = GrandTotal = Total + Tip;            
        }
    }
}
