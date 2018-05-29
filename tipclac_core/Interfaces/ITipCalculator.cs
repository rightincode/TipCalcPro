namespace tipcalc_core.Interfaces
{
    public interface ITipCalculator
    {
        decimal Total { get; set; }
        decimal Tip { get; set; }
        decimal TipPercent { get; set; }
        decimal GrandTotal { get; }

        void CalcTip();

        void CalcTipPercentage();

        void RoundTip();

        void UnRoundTip();

        void Reset();
    }
}
