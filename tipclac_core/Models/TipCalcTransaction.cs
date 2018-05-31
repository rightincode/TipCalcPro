using System;
using SQLite;

using tipcalc_core.Interfaces;

namespace tipcalc_core.Models
{
    public class TipCalcTransaction : ITipCalcTransaction
    {
        [PrimaryKey, AutoIncrement]
        public decimal Id { get ; set; }

        public decimal Total { get ; set ; }
        public decimal Tip { get ; set ; }
        public decimal TipPercent { get ; set ; }
        public decimal GrandTotal { get ; set ; }
        public bool Split { get ; set ; }
        public int NumOfPersons { get ; set ; }
        public decimal TotalPerPerson { get ; set ; }
        public DateTime Saved { get ; set ; }
        
        public TipCalcTransaction()
        {

        }
    }
}
