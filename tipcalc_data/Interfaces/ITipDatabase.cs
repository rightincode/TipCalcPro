using System.Collections.Generic;
using System.Threading.Tasks;
using tipcalc_core.Interfaces;
using tipcalc_core.Models;

namespace tipcalc_data.Interfaces
{
    public interface ITipDatabase
    {
        Task<List<TipCalcTransaction>> GetTipCalcTransactionsAsync();
        Task<TipCalcTransaction> GetTipCalcTransactionAsync(int id);
        Task<int> SaveTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction);
        Task<int> DeleteTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction);
    }
}
