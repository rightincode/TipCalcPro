using SQLite;
using System;

namespace tipcalc_data.Interfaces
{
    public interface ITipCalcTransactionDO
    {
        [PrimaryKey, AutoIncrement]
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
