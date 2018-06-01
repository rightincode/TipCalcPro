using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;
using tipcalc_core.Interfaces;
using tipcalc_core.Models;
using tipcalc_data.Interfaces;

namespace tipcalc_data.Models
{
    public class TipDatabase : ITipDatabase
    {
        private SQLiteAsyncConnection _databaseConnection;

        public TipDatabase(IFileHelper fileHelper)
        {
            _databaseConnection = new SQLiteAsyncConnection(fileHelper.GetLocalFilePath("TipCalcTransactions.db3"));

            try
            {
                _databaseConnection.CreateTableAsync<TipCalcTransaction>().Wait();
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.Message);
            };            
        }

        public Task<int> DeleteTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction)
        {
            return _databaseConnection.DeleteAsync(tipCalcTransaction);
        }

        public Task<TipCalcTransaction> GetTipCalcTransactionAsync(int id)
        {
            return _databaseConnection.Table<TipCalcTransaction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<TipCalcTransaction>> GetTipCalcTransactionsAsync()
        {
            return _databaseConnection.Table<TipCalcTransaction>().ToListAsync();
        }

        public Task<int> SaveTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction)
        {
            if (tipCalcTransaction.Id > 0)
            {
                return _databaseConnection.UpdateAsync(tipCalcTransaction);
            }
            else
            {
                return _databaseConnection.InsertAsync(tipCalcTransaction);
            }
        }
    }
}
