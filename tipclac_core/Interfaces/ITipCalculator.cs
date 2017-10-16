using System;
using System.Collections.Generic;
using System.Text;

namespace tipclac_core.Interfaces
{
    interface ITipCalculator
    {
        double Total { get; set; }
        double Tip { get; }
        double GrandTotal { get; }

        void CalcTip(double tipPercent);
    }
}
