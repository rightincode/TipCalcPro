using System;
using SQLite;

using tipcalc_data.Interfaces;

namespace tipcalc_data.Models
{
    public class TipCalcTransactionDO : ITipCalcTransactionDO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get ; set; }

        public decimal Total { get ; set ; }
        public decimal Tip { get ; set ; }
        public decimal TipPercent { get ; set ; }
        public decimal GrandTotal { get ; set ; }
        public bool Split { get ; set ; }
        public int NumOfPersons { get ; set ; }
        public decimal TotalPerPerson { get ; set ; }
        public DateTime Saved { get ; set ; }
        
        public TipCalcTransactionDO()
        {

        }
    }
}
