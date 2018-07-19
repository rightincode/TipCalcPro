using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using tipcalc_core.Interfaces;
using tipcalc_data.Interfaces;
using tipcalcapp.Validations;
using tipcalcapp.Messages;
using Xamarin.Forms;

namespace tipcalcapp.ViewModels
{
    public class CalculatorPageViewModel : ExtendedBindableObject, INotifyPropertyChanged
    {
        private ValidatableObject<string> _totalText;
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

            _totalText = new ValidatableObject<string>();

            AddValidations();

            Task.Run(async () => {
                SendTipSavedMessage(await GetCurrentCountOfSavedTipTransactions());
            });            
        }

        public List<TipPercentage> TipPresets { get; } = new List<TipPercentage>
        {
            new TipPercentage{ TipPercentageTxt = "15%", TipPercentageValue = 15},
            new TipPercentage{ TipPercentageTxt = "18%", TipPercentageValue = 18},
            new TipPercentage{ TipPercentageTxt = "20%", TipPercentageValue = 20},
            new TipPercentage{ TipPercentageTxt = "22%", TipPercentageValue = 22},
        };

        public List<string> TotalTxtErrors
        {
            get { return _totalText.Errors; }
        }

        public string TotalTxt
        {
            get
            {
                return (!string.IsNullOrEmpty(_totalText.Value)) ? _totalText.Value : _calculator.Total.ToString();
            }
            set
            {
                _totalText.Value = value;

                try
                {
                    _calculator.Total = decimal.Parse(_totalText.Value);
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
            get { return _calculator.NumberOfPersons; }
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

            RaisePropertyChanged(() => TipTxt);
            RaisePropertyChanged(() => GrandTotalTxt);
            RaisePropertyChanged(() => NumberOfPersons);
            RaisePropertyChanged(() => TotalPerPersonTxt);
        }

        public void UnRoundTotal()
        {
            _calculator.UnRoundTotal();

            RaisePropertyChanged(() => TipTxt);
            RaisePropertyChanged(() => GrandTotalTxt);
            RaisePropertyChanged(() => NumberOfPersons);
            RaisePropertyChanged(() => TotalPerPersonTxt);
        }

        public ICommand ValidateTotalTxtCommand => new Command(() => ValidateTotalTxt());

        public ICommand ResetCalculatorCommand => new Command(() => ResetCalculator());

        public ICommand SaveTipTransactionCommand => new Command(async () =>
        {
            int result = await SaveTipTransaction();

            if (result > 0)
            {
                ResetCalculator();
            }
        });
        
        private void SplitGrandTotal()
        {
            _calculator.SplitGrandTotal();

            RaisePropertyChanged(() => NumberOfPersons);
            RaisePropertyChanged(() => TotalPerPersonTxt);
        }

        public void ResetCalculator()
        {
            _calculator.Reset();
            TotalTxt = _calculator.Total.ToString();

            RaisePropertyChanged(() => TotalTxt);
            RaisePropertyChanged(() => TipTxt);
            RaisePropertyChanged(() => TipPercentTxt);
            RaisePropertyChanged(() => TipPercent);
            RaisePropertyChanged(() => GrandTotalTxt);
            RaisePropertyChanged(() => NumberOfPersons);
            RaisePropertyChanged(() => TotalPerPersonTxt);
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

            int saveResult = await _tipDatabase.SaveTipCalcTransactionAsync(_tipCalcTransaction);

            SendTipSavedMessage(await GetCurrentCountOfSavedTipTransactions());
            
            return saveResult; 
        }

        private void CalculateTipPropertyChangedNotifications()
        {
            RaisePropertyChanged(() => TotalTxtErrors);
            RaisePropertyChanged(() => TipTxt);
            RaisePropertyChanged(() => TipPercentTxt);
            RaisePropertyChanged(() => GrandTotalTxt);
            RaisePropertyChanged(() => NumberOfPersons);
            RaisePropertyChanged(() => TotalPerPersonTxt);
        }

        private void CalculateTipPercentagePropertyChangedNotifications()
        {
            RaisePropertyChanged(() => TipPercentTxt);
            RaisePropertyChanged(() => GrandTotalTxt);
            RaisePropertyChanged(() => NumberOfPersons);
            RaisePropertyChanged(() => TotalPerPersonTxt);
        }

        private void AddValidations()
        {
            _totalText.Validations.Add(new IsPositiveNumericValueRule<string> { ValidationMessage = "Must be greater than zero." });
            //_totalText.Validations.Add(new IsNonNegativeNumericValueRule<string> { ValidationMessage = "Cannot be negative." });
        }
        
        private bool ValidateTotalTxt()
        {
            return _totalText.Validate();
        }

        private async Task<int> GetCurrentCountOfSavedTipTransactions()
        {
            var tipTransactions = await _tipDatabase.GetTipCalcTransactionsAsync();
            return tipTransactions.Count;
        }

        private void SendTipSavedMessage(int tipCount)
        {
            MessagingCenter.Send(this, MessageKeys.SaveTip, tipCount);
        }
    }
}
