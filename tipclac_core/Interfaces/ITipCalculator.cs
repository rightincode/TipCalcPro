namespace tipcalc_core.Interfaces
{
    public interface ITipCalculator
    {
        decimal Total { get; set; }
        decimal Tip { get; set; }
        decimal TipPercent { get; set; }
        decimal GrandTotal { get; }        
        int NumberOfPersons { get; set; }
        decimal TotalPerPerson { get; }

        void CalcTip();

        void CalcTipPercentage();

        void RoundTip();

        void UnRoundTip();

        void SplitGrandTotal();

        void Reset();
    }
}
