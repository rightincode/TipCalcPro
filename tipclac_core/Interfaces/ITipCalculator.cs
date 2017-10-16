namespace tipcalc_core.Interfaces
{
    public interface ITipCalculator
    {
        double Total { get; set; }
        double Tip { get; set; }
        double TipPercent { get; set; }
        double GrandTotal { get; }

        void CalcTip(double tipPercent);
    }
}
