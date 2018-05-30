namespace tipcalc_core.Interfaces
{
    public interface ITipCalculator
    {
        decimal Total { get; set; }
        decimal Tip { get; set; }
        decimal SavedTip { get; }
        decimal TipPercent { get; set; }
        decimal GrandTotal { get; }
        decimal SavedGrandTotal { get; }
        int NumberOfPersons { get; set; }
        decimal TotalPerPerson { get; }

        void CalcTip();

        void CalcTipPercentage();

        void RoundTotal();

        void UnRoundTotal();

        void SplitGrandTotal();

        void Reset();
    }
}
