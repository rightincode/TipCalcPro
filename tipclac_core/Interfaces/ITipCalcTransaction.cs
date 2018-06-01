using System;

namespace tipcalc_core.Interfaces
{
    public interface ITipCalcTransaction
    {
        int Id { get; set; }

        decimal Total { get; set; }
        decimal Tip { get; set; }
        decimal TipPercent { get; set; }
        decimal GrandTotal { get; set; }

        bool Split { get; set; }
        int NumOfPersons { get; set; }
        decimal TotalPerPerson { get; set; }

        DateTime Saved { get; set; }
    }
}
