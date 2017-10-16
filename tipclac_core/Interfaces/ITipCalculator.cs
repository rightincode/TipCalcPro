namespace tipcalc_core.Interfaces
{
    public interface ITipCalculator
    {
        double Total { get; set; }
        double Tip { get; }
        double GrandTotal { get; }

        void CalcTip(double tipPercent);
    }
}
