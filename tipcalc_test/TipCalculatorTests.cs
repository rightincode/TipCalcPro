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
            var tipCalculator = new TipCalculator();

            tipCalculator.Total = 10;
            tipCalculator.TipPercent = 0;

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, 0);
        }

        [TestMethod]
        public void CalculateTipTenPercent()
        {
            var tipCalculator = new TipCalculator();

            tipCalculator.Total = 10;
            tipCalculator.TipPercent = 10;

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, 1.00);
        }

        [TestMethod]
        public void CalculateTipTwentyFivePercent()
        {
            var tipCalculator = new TipCalculator();

            tipCalculator.Total = 10;
            tipCalculator.TipPercent = 25;

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, 2.50);
        }

        [TestMethod]
        public void CalculateTipOneHundredPercent()
        {
            var tipCalculator = new TipCalculator();

            tipCalculator.Total = 10;
            tipCalculator.TipPercent = 100;

            tipCalculator.CalcTip();

            Assert.AreEqual(tipCalculator.Tip, 10);
        }
    }
}
