using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using tipcalc_core.Interfaces;
using tipcalc_data.Interfaces;
using Xamarin.Forms;

namespace tipcalcapp.ViewModels
{
    public class CalculatorPageViewModel : INotifyPropertyChanged
    {
        private string totalTxt;
        private string tipTxt;
        private readonly ITipCalculator _calculator;
        private readonly ITipDatabase _tipDatabase;
        private ITipCalcTransaction _tipCalcTransaction;
        
        public CalculatorPageViewModel(ITipCalculator tipCalculator, 
            ITipCalcTransaction tipCalcTransaction,
            ITipDatabase tipDatabase)
        {
            _calculator = tipCalculator;
            _tipCalcTransaction = tipCalcTransaction;
            _tipDatabase = tipDatabase;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<TipPercentage> TipPresets { get; } = new List<TipPercentage>
        {
            new TipPercentage{ TipPercentageTxt = "15%", TipPercentageValue = 15},
            new TipPercentage{ TipPercentageTxt = "18%", TipPercentageValue = 18},
            new TipPercentage{ TipPercentageTxt = "20%", TipPercentageValue = 20},
            new TipPercentage{ TipPercentageTxt = "22%", TipPercentageValue = 22},
        };

        public string TotalTxt
        {
            get
            {
                return _calculator.Total.ToString();
            }

            set
            {
                totalTxt = value;

                try
                {
                    string newValue = value;
                    _calculator.Total = decimal.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Total = 0;
                }
                finally
                {
                    _calculator.CalcTip();
                    CalculateTipPropertyChangedNotifications();
                }
            }
        }

        public string TipTxt
        {
            get { return _calculator.Tip.ToString(); }
            set
            {
                tipTxt = value;

                try
                {
                    string newValue = value;
                    _calculator.Tip = decimal.Parse(newValue);
                }
                catch (Exception)
                {
                    _calculator.Tip = 0;
                }
                finally
                {
                    _calculator.CalcTipPercentage();
                    CalculateTipPercentagePropertyChangedNotifications();
                }
            }
        }
        
        public string TipPercentTxt
        {
            get { return _calculator.TipPercent.ToString(); }
        }

        public decimal TipPercent
        {
            get { return _calculator.TipPercent; }
            set {
                _calculator.TipPercent = value;
                _calculator.CalcTip();
                CalculateTipPropertyChangedNotifications();
            }
        }

        public string GrandTotalTxt
        {
            get { return _calculator.GrandTotal.ToString("F2"); }
        }

        public int NumberOfPersons
        {
            get { return _calculator.NumberOfPersons;  }
            set
            {
                _calculator.NumberOfPersons = value;
                SplitGrandTotal();
            }
        }

        public string TotalPerPersonTxt
        {
            get { return _calculator.TotalPerPerson.ToString("F2"); }
        }

        public void RoundTotal()
        {
            _calculator.RoundTotal();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfPersons"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPerPersonTxt"));
        }

        public void UnRoundTotal()
        {
            _calculator.UnRoundTotal();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfPersons"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPerPersonTxt"));
        }

        private void SplitGrandTotal()
        {
            _calculator.SplitGrandTotal();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfPersons"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPerPersonTxt"));
        }

        public void ResetCalculator()
        {
            _calculator.Reset();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercent"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfPersons"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPerPersonTxt"));
        }

        public async Task<int> SaveTipTransaction()
        {
            _tipCalcTransaction.Id = 0;
            _tipCalcTransaction.GrandTotal = _calculator.GrandTotal;
            _tipCalcTransaction.NumOfPersons = _calculator.NumberOfPersons;
            _tipCalcTransaction.Saved = DateTime.UtcNow;            
            _tipCalcTransaction.Split = (NumberOfPersons > 1 ) ? true : false;
            _tipCalcTransaction.Tip = _calculator.Tip;
            _tipCalcTransaction.TipPercent = _calculator.TipPercent;
            _tipCalcTransaction.Total = _calculator.Total;
            _tipCalcTransaction.TotalPerPerson = _calculator.TotalPerPerson;
            
            await _tipDatabase.SaveTipCalcTransactionAsync(_tipCalcTransaction);
            return _tipCalcTransaction.Id;
        }

        private void CalculateTipPropertyChangedNotifications()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfPersons"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPerPersonTxt"));
        }

        private void CalculateTipPercentagePropertyChangedNotifications()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TipPercentTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrandTotalTxt"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfPersons"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPerPersonTxt"));
        }

        
    }
}
