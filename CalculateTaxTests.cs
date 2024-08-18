using CalculateTax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CalculateTaxTests
{
    [TestClass()]
    public class ProgramTests
    {
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void CalculateTaxTest(decimal income, decimal expected)
        {

            var theTaxGap = Program.CreatList();
            decimal tax = Program.CalculateTax(income, theTaxGap);
            Assert.AreEqual(expected, tax, $"測試失敗，收入: {income}, 預期稅額: {expected}, 實際稅額: {tax}");
        }

        private static IEnumerable<object[]> GetData()
        {
            yield return new object[] { 100_000M, 5_000M };
            yield return new object[] { 540_000M, 27_000M };
            yield return new object[] { 540_001M, 27_000.12M };
            yield return new object[] { 1_210_000M, 107_400M };
            yield return new object[] { 1_218_000M, 109_000M };
            yield return new object[] { 2_420_000M, 349_400M };
            yield return new object[] { 2_500_000M, 373_400M };
            yield return new object[] { 4_530_000M, 982_400M };
            yield return new object[] { 5_530_000M, 1_382_400M };
            yield return new object[] { 10_310_000M, 3_294_400M };
            yield return new object[] { 10_710_000M, 3_494_400M };
        }
    }

}
