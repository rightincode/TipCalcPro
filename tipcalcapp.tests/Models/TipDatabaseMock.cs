using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tipcalc_core.Interfaces;
using tipcalc_core.Models;
using tipcalc_data.Interfaces;

namespace tipcalcapp.tests.Models
{
    public class TipDatabaseMock : ITipDatabase
    {
        private readonly List<TipCalcTransaction> _database = new List<TipCalcTransaction>
        {
            new TipCalcTransaction
            {
                Id = 1,
                GrandTotal = 110,
                NumOfPersons = 1,
                Saved = DateTime.UtcNow,
                Split = false,
                Tip = 10,
                TipPercent = 10,
                Total = 100,
                TotalPerPerson = 110
            }
        };

        private readonly IFileHelper _fileHelper;

        public TipDatabaseMock(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;       
        }
        
        public Task<int> DeleteTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction)
        {
            return Task.Run(() =>
            {
                if ((tipCalcTransaction.Id > 0) && (_database.Exists(tct => tct.Id == tipCalcTransaction.Id)))
                {
                    _database.Remove((TipCalcTransaction)tipCalcTransaction);
                    return 1;
                }

                return 0;
            });
        }

        public Task<TipCalcTransaction> GetTipCalcTransactionAsync(int id)
        {
            return Task.Run(() =>
            {
                return _database.Find(tct => tct.Id == id);
            });
        }

        public Task<List<TipCalcTransaction>> GetTipCalcTransactionsAsync()
        {
            return Task.Run(() =>
            {
                return _database;
            });
        }

        public Task<int> SaveTipCalcTransactionAsync(ITipCalcTransaction tipCalcTransaction)
        {
            return Task.Run(() =>
            {
                if ((tipCalcTransaction.Id > 0) && (_database.Exists(tct => tct.Id == tipCalcTransaction.Id)))
                {
                    var itemIndex = _database.FindIndex(tct => tct.Id == tipCalcTransaction.Id);
                    _database[itemIndex] = (TipCalcTransaction)tipCalcTransaction;                    
                }
                else
                {
                    tipCalcTransaction.Id = GetNextTransactionId();
                    _database.Add((TipCalcTransaction)tipCalcTransaction);
                }

                return tipCalcTransaction.Id;
            });            
        }

        private int GetNextTransactionId()
        {
            int nextId = 0;

            _database.ForEach(tipCalcTransaction =>
            {
                if (tipCalcTransaction.Id > nextId)
                {
                    nextId = tipCalcTransaction.Id;
                }
            });
                        
            return nextId + 1;
        }
    }
}
