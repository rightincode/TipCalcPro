using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual(tipCalculator.Tip, 1.00);
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

            Assert.AreEqual(tipCalculator.Tip, 2.50);
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
                Tip = 2.50
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
    }
}
