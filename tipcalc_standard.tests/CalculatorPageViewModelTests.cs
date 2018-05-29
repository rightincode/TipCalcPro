using Microsoft.VisualStudio.TestTools.UnitTesting;

using tipcalc_core.Models;
using tipcalc_standard.ViewModels;

namespace tipcalc_standard.tests
{
    [TestClass]
    public class CalculatorPageViewModelTests
    {
        [TestMethod]
        public void SetTotalText_ValidPositiveNumericalEntry_CalculatorContainsMatchingTotalDecimalValue()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "55.34"
            };

            Assert.AreEqual(decimal.Parse(myCalculatorViewModel.TotalTxt), myCalculator.Total);

        }

        [TestMethod]
        public void SetTotalText_ValidNegativeNumericalEntry_CalculatorContainsMatchingTotalDecimalValue()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "-55.34"
            };

            Assert.AreEqual(decimal.Parse(myCalculatorViewModel.TotalTxt), myCalculator.Total);

        }

        [TestMethod]
        public void SetTotalText_InValidEntry_CalculatorContainsZeroForTotal()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "ABCD"
            };

            Assert.AreEqual(0, myCalculator.Total);

        }

        [TestMethod]
        public void SetTipText_ValidPositiveNumericalEntry_CalculatorContainsMatchingTipDecimalValue()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TipTxt = "5.34"
            };

            Assert.AreEqual(decimal.Parse(myCalculatorViewModel.TipTxt), myCalculator.Tip);

        }

        [TestMethod]
        public void SetTipText_ValidNegativeNumericalEntry_CalculatorContainsMatchingTipDecimalValue()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TipTxt = "-5.34"
            };

            Assert.AreEqual(decimal.Parse(myCalculatorViewModel.TipTxt), myCalculator.Tip);

        }

        [TestMethod]
        public void SetTipText_InValidEntry_CalculatorContainsZeroForTip()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TipTxt = "ABCD"
            };

            Assert.AreEqual(0, myCalculator.Tip);

        }

        [TestMethod]
        public void SetTipPercent_ValidPositiveNumericalEntry_CalculatorContainsMatchingTipDecimalValue()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TipPercent = 10
            };

            Assert.AreEqual(myCalculatorViewModel.TipPercent, myCalculator.TipPercent);
        }

        [TestMethod]
        public void SetTipPercent_ValidNegativeNumericalEntry_CalculatorContainsMatchingTipDecimalValue()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TipPercent = -10
            };

            Assert.AreEqual(myCalculatorViewModel.TipPercent, myCalculator.TipPercent);
        }

        [TestMethod]
        public void Calculate10PercentTipOn100Total_ValidInput_TipIs10Dollars_TotalIs110Dollars()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "100",
                TipPercent = 10
            };
                        
            Assert.AreEqual("10.0", myCalculatorViewModel.TipTxt);
            Assert.AreEqual(10, myCalculatorViewModel.TipPercent);
            Assert.AreEqual("10", myCalculatorViewModel.TipPercentTxt);
            Assert.AreEqual("110.00", myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void Calculate18PercentTipOn35Total_ValidInput_TipIs6_30Dollars_TotalIs41_30Dollars()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "35",
                TipPercent = 18
            };

            Assert.AreEqual("6.30", myCalculatorViewModel.TipTxt);
            Assert.AreEqual(18, myCalculatorViewModel.TipPercent);
            Assert.AreEqual("18", myCalculatorViewModel.TipPercentTxt);
            Assert.AreEqual("41.30", myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void CalculateTipPercentageFor10DollarTipOn100Total_ValidInput_TipPercentageIs10_TotalIs110Dollars()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "100",
                TipTxt = "10"
            };

            Assert.AreEqual("10", myCalculatorViewModel.TipTxt);
            Assert.AreEqual(10, myCalculatorViewModel.TipPercent);
            Assert.AreEqual("10.0", myCalculatorViewModel.TipPercentTxt);
            Assert.AreEqual("110.00", myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void CalculateTipPercentFor6_30On35Total_ValidInput_TipPercentageIs18_TotalIs41_30Dollars()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "35",
                TipTxt = "6.30"
            };

            Assert.AreEqual("6.30", myCalculatorViewModel.TipTxt);
            Assert.AreEqual(18, myCalculatorViewModel.TipPercent);
            Assert.AreEqual("18.0", myCalculatorViewModel.TipPercentTxt);
            Assert.AreEqual("41.30", myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void ResetCalulator_ValidViewModel_CalculatorResetToInitialState()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "35",
                TipTxt = "6.30"
            };

            var newCalculator = new TipCalculator();
            var newCalculatorViewModel = new CalculatorPageViewModel(myCalculator);
            
            myCalculatorViewModel.ResetCalculator();

            Assert.AreEqual(newCalculatorViewModel.TotalTxt, myCalculatorViewModel.TotalTxt);
            Assert.AreEqual(newCalculatorViewModel.TipTxt, myCalculatorViewModel.TipTxt);
            Assert.AreEqual(newCalculatorViewModel.TipPercent, myCalculatorViewModel.TipPercent);
            Assert.AreEqual(newCalculatorViewModel.TipPercentTxt, myCalculatorViewModel.TipPercentTxt);
            Assert.AreEqual(newCalculatorViewModel.GrandTotalTxt, myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void RoundTip_Total49_36TipPercent15_GrandTotal172()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "149.36",
                TipPercent = 15
            };

            myCalculatorViewModel.RoundTip();

            Assert.AreEqual("172.00", myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void RoundTip_Total49_36TipPercent18_GrandTotal176()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "149.36",
                TipPercent = 18
            };

            myCalculatorViewModel.RoundTip();

            Assert.AreEqual("176.00", myCalculatorViewModel.GrandTotalTxt);
        }

        [TestMethod]
        public void RoundTipThenUnRoundTip_Total49_36TipPercent15_GrandTotal171_76()
        {
            var myCalculator = new TipCalculator();
            var myCalculatorViewModel = new CalculatorPageViewModel(myCalculator)
            {
                TotalTxt = "149.36",
                TipPercent = 15
            };

            myCalculatorViewModel.RoundTip();
            myCalculatorViewModel.UnRoundTip();

            Assert.AreEqual("171.76", myCalculatorViewModel.GrandTotalTxt);
        }
    }
}
