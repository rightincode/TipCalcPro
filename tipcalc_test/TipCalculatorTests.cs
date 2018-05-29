using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tipcalc_core.Models;

namespace tipcalc_core.tests
{
    [TestClass]
    public class TipCalculatorTests
    {
        [TestMethod]
        public void CalculateTipZeroPercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                TipPercent = 0
            };

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, 0);
        }

        [TestMethod]
        public void CalculateTipTenPercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                TipPercent = 10
            };

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, (decimal)1.00);
        }

        [TestMethod]
        public void CalculateTipTwentyFivePercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                TipPercent = 25
            };

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, (decimal)2.50);
        }

        [TestMethod]
        public void CalculateTipOneHundredPercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                TipPercent = 100
            };

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, 10);
        }

        [TestMethod]
        public void CalculateTipPercentageZeroPercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                Tip = 0
            };

            tipCalculator.CalcTipPercentage();

            Assert.AreEqual(tipCalculator.TipPercent, 0);
        }

        [TestMethod]
        public void CalculateTipPercentageTenPercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                Tip = 1
            };

            tipCalculator.CalcTipPercentage();

            Assert.AreEqual(tipCalculator.TipPercent, 10);
        }

        [TestMethod]
        public void CalculateTipPercentageTwentyFivePercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                Tip = (decimal)2.50
            };

            tipCalculator.CalcTipPercentage();

            Assert.AreEqual(tipCalculator.TipPercent, 25);
        }

        [TestMethod]
        public void CalculateTipPercentageOneHundredPercent()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                Tip = 10
            };

            tipCalculator.CalcTipPercentage();

            Assert.AreEqual(tipCalculator.TipPercent, 100);
        }

        [TestMethod]
        public void CalculateGrandTotal()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                TipPercent = 50
            };

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.GrandTotal, 15);
        }

        [TestMethod]
        public void ResetCalculator_CalculatorPopulatedWithValidValues_CalculatorResetToOriginalState()
        {
            var tipCalculator = new TipCalculator
            {
                Total = 10,
                TipPercent = 50
            };

            var newTipCalculator = new TipCalculator();

            tipCalculator.CalcTip();
            tipCalculator.Reset();

            Assert.AreEqual(newTipCalculator.GrandTotal, tipCalculator.GrandTotal);
            Assert.AreEqual(newTipCalculator.Tip, tipCalculator.Tip);
            Assert.AreEqual(newTipCalculator.TipPercent, tipCalculator.TipPercent);
            Assert.AreEqual(newTipCalculator.Total, tipCalculator.Total);
            Assert.AreEqual(newTipCalculator.NumberOfPersons, tipCalculator.NumberOfPersons);
            Assert.AreEqual(newTipCalculator.SavedGrandTotal, tipCalculator.SavedGrandTotal);
            Assert.AreEqual(newTipCalculator.TotalPerPerson, tipCalculator.TotalPerPerson);

        }

        [TestMethod]
        public void RoundTip_Total49_36TipPercent15_GrandTotal172()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 15
            };

            tipCalculator.CalcTip();
            tipCalculator.RoundTip();

            Assert.AreEqual((decimal)172.00, tipCalculator.GrandTotal);

        }

        [TestMethod]
        public void RoundTip_Total49_36TipPercent18_GrandTotal176()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 18
            };

            tipCalculator.CalcTip();
            tipCalculator.RoundTip();

            Assert.AreEqual((decimal)176.00, tipCalculator.GrandTotal);
        }

        [TestMethod]
        public void RoundTipThenUnRound_Total49_36TipPercent15_GrandTotal171_76()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 15
            };

            tipCalculator.CalcTip();
            tipCalculator.RoundTip();
            tipCalculator.UnRoundTip();

            Assert.AreEqual((decimal)171.76, tipCalculator.GrandTotal);
        }

        [TestMethod]
        public void RoundTipThenUnRound_Total49_36TipPercent15_GrandTotalEqualsSavedGrandTotal()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 15
            };

            tipCalculator.RoundTip();
            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.SavedGrandTotal, tipCalculator.GrandTotal);
        }

        [TestMethod]
        public void SplitCheck_OnePerson_TotalPersonEqualsGrandTotal()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 15,
                NumberOfPersons = 1
            };

            tipCalculator.CalcTip();
            tipCalculator.SplitGrandTotal();

            Assert.AreEqual(tipCalculator.TotalPerPerson, tipCalculator.GrandTotal);

        }

        [TestMethod]
        public void SplitCheck_TwoPersons_TotalPersonEqualsOneHalfGrandTotal()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 15,
                NumberOfPersons = 2
            };

            tipCalculator.CalcTip();
            tipCalculator.SplitGrandTotal();

            Assert.AreEqual(tipCalculator.TotalPerPerson, Math.Round((tipCalculator.GrandTotal/2), 2));

        }

        [TestMethod]
        public void SplitCheck_ThreePersons_TotalPersonEqualsOneHalfGrandTotal()
        {
            var tipCalculator = new TipCalculator
            {
                Total = (decimal)149.36,
                TipPercent = 15,
                NumberOfPersons = 3
            };

            tipCalculator.CalcTip();
            tipCalculator.SplitGrandTotal();

            Assert.AreEqual(tipCalculator.TotalPerPerson, Math.Round((tipCalculator.GrandTotal / 3), 2));

        }

    }
}
