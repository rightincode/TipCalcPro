using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using tipcalc_core.Models;
using tipcalcapp.tests.Models;
using tipcalcapp.ViewModels;

namespace tipcalcapp.tests
{
    [TestClass]
    public class TipHistoryPageViewModelTests
    {
        TipCalcTransaction myTipCalcTransaction;
        TipDatabaseMock myTipDatabase;

        [TestInitialize]
        public void Initialize()
        {
            myTipCalcTransaction = new TipCalcTransaction();
            myTipDatabase = new TipDatabaseMock(new FileHelperMock());
        }

        [TestMethod]
        public async Task LoadViewModelHistory_ValidTipHistory_ListOfTipHistoryTransactions()
        {
            var myTipHistoryViewModel = new TipHistoryPageViewModel(myTipDatabase);

            await myTipHistoryViewModel.LoadTipHistory();

            Assert.IsNotNull(myTipHistoryViewModel.TipCalcTransactions);
            Assert.IsTrue(myTipHistoryViewModel.TipCalcTransactions.Count > 0);
        }

        [TestMethod]
        public async Task LoadViewModelHistory_ValidTipHistory_ListOfTipHistoryTransactionsInDecendingOrderBySaved()
        {
            var myTipHistoryViewModel = new TipHistoryPageViewModel(myTipDatabase);

            await myTipHistoryViewModel.LoadTipHistory();

            Assert.AreEqual(2, myTipHistoryViewModel.TipCalcTransactions[0].Id);
        }
    }
}
