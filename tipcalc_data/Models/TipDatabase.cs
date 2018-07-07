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
                _databaseConnection.CreateTableAsync<TipCalcTransactionDO>().Wait();
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.Message);
            };            
        }

        public Task<int> DeleteTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction)
        {
            return _databaseConnection.DeleteAsync(TransformToTipCalcTransactionDO(tipCalcTransaction));
        }

        public Task<TipCalcTransaction> GetTipCalcTransactionAsync(int id)
        {
            return Task.Run(async () =>
            {
                TipCalcTransactionDO tipCalcTransactionDO = 
                    await _databaseConnection.Table<TipCalcTransactionDO>().Where(i => i.Id == id).FirstOrDefaultAsync();

                return TransformToTipCalcTransaction(tipCalcTransactionDO);
            });
        }

        public Task<List<TipCalcTransaction>> GetTipCalcTransactionsAsync()
        {
            return Task.Run(async () =>
            {
                List<TipCalcTransactionDO> tipCalcTransactionDOs =
                    await _databaseConnection.Table<TipCalcTransactionDO>().OrderByDescending(tct => tct.Saved).ToListAsync();

                List<TipCalcTransaction> tipCalcTransactions = new List<TipCalcTransaction>();

                foreach(TipCalcTransactionDO tipCalcTransactionDO in tipCalcTransactionDOs)
                {
                    tipCalcTransactions.Add(TransformToTipCalcTransaction(tipCalcTransactionDO));
                }

                return tipCalcTransactions;
            });
        }

        public Task<int> SaveTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction)
        {
            if (tipCalcTransaction.Id > 0)
            {
                return _databaseConnection.UpdateAsync(TransformToTipCalcTransactionDO(tipCalcTransaction));
            }
            else
            {
                return _databaseConnection.InsertAsync(TransformToTipCalcTransactionDO(tipCalcTransaction));
            }
        }

        private TipCalcTransactionDO TransformToTipCalcTransactionDO(ITipCalcTransaction tipCalcTransaction)
        {
            var tipCalcTransactionDO = new TipCalcTransactionDO
            {
                Id = tipCalcTransaction.Id,
                Total = tipCalcTransaction.Total,
                Tip = tipCalcTransaction.Tip,
                TipPercent = tipCalcTransaction.TipPercent,
                GrandTotal = tipCalcTransaction.GrandTotal,
                Split = tipCalcTransaction.Split,
                NumOfPersons = tipCalcTransaction.NumOfPersons,
                TotalPerPerson = tipCalcTransaction.TotalPerPerson,
                Saved = tipCalcTransaction.Saved
            };

            return tipCalcTransactionDO;
        }

        private TipCalcTransaction TransformToTipCalcTransaction(ITipCalcTransactionDO tipCalcTransactionDO)
        {
            var tipCalcTransaction = new TipCalcTransaction
            {
                Id = tipCalcTransactionDO.Id,
                Total = tipCalcTransactionDO.Total,
                Tip = tipCalcTransactionDO.Tip,
                TipPercent = tipCalcTransactionDO.TipPercent,
                GrandTotal = tipCalcTransactionDO.GrandTotal,
                Split = tipCalcTransactionDO.Split,
                NumOfPersons = tipCalcTransactionDO.NumOfPersons,
                TotalPerPerson = tipCalcTransactionDO.TotalPerPerson,
                Saved = tipCalcTransactionDO.Saved
            };

            return tipCalcTransaction;
        }

    }
}
