using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using tipcalc_core.Interfaces;
using tipcalc_data.Interfaces;

namespace tipcalcapp.ViewModels
{
    public class TipHistoryPageViewModel : INotifyPropertyChanged
    {
        private readonly ITipDatabase _tipDatabase;

        public ObservableCollection<ITipCalcTransaction> TipCalcTransactions { get; private set; }

        public TipHistoryPageViewModel(ITipDatabase tipDatabase)
        {
            _tipDatabase = tipDatabase;
            TipCalcTransactions = new ObservableCollection<ITipCalcTransaction>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task LoadTipHistory()
        {
            TipCalcTransactions = new ObservableCollection<ITipCalcTransaction>(await _tipDatabase.GetTipCalcTransactionsAsync());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipCalcTransactions"));
        }
    }
}
