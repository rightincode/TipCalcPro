using System;
using tipcalc_core.Interfaces;

namespace tipcalc_core.Models
{
    public class TipCalculator : ITipCalculator
    {
        private decimal tipPercent;

        public TipCalculator()
        {
            NumberOfPersons = 1;
        }

        public decimal Total { get; set; }

        public decimal Tip { get; set; }

        public decimal SavedTip { get; set; }

        public decimal TipPercent
        {
            get { return tipPercent; }
            set { tipPercent = Math.Round(value); }
        }

        public decimal SavedGrandTotal { get; private set; }

        public decimal GrandTotal { get; private set; }

        public int NumberOfPersons { get; set; }

        public decimal TotalPerPerson { get; private set; }

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
            SavedTip = Tip;
            GrandTotal = Math.Round(GrandTotal);
            Tip = (GrandTotal > Total) ? GrandTotal - Total : 0 ;
            SplitGrandTotal();
        }

        public void UnRoundTip()
        {
            GrandTotal = SavedGrandTotal;
            SavedGrandTotal = 0;
            Tip = SavedTip;
            SavedTip = 0;
            SplitGrandTotal();
        }

        public void SplitGrandTotal()
        {
            TotalPerPerson = Math.Round((GrandTotal / NumberOfPersons), 2);
        }

        public void Reset()
        {
            Total = 0;
            Tip = 0;
            TipPercent = 0;
            GrandTotal = 0;
            NumberOfPersons = 1;
            TotalPerPerson = 0;
            SavedGrandTotal = 0;
            SavedTip = 0;
        }
        
        private void UpdateGrandTotal()
        {
            SavedGrandTotal = GrandTotal = Total + Tip;
            SavedTip = Tip;
            SplitGrandTotal();
        }
    }
}
