using tipcalc_core.Interfaces;

namespace tipcalc_core.Models
{
    public class TipCalculator : ITipCalculator
    {
        private double total;
        private double tip;
        private double grandTotal;

        public double Total {
            get { return total; }
            set { total = value; }
        }

        public double Tip
        {
            get { return tip; }
        }

        public double GrandTotal
        {
            get { return grandTotal; }
        }

        public void CalcTip(double tipPercent)
        {
            if (tipPercent > 0)
            {
                tip = total * (tipPercent / 100);                
            }
            else
            {
                tip = 0;
            }
            UpdateGrandTotal();
        }
        
        private void UpdateGrandTotal()
        {
            grandTotal = total + tip;
        }
    }
}
