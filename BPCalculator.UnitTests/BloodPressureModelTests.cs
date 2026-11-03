using BPCalculator.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BPCalculator.UnitTests
{
    [TestClass]
    public sealed class BloodPressureModelTests
    {
        [TestMethod]
        public void OnGet_SetsDefaultReading()
        {
            var page = new BloodPressureModel();

            page.OnGet();

            Assert.AreEqual(100, page.BP.Systolic);
            Assert.AreEqual(60, page.BP.Diastolic);
        }

        [TestMethod]
        public void OnPost_ValidReading_ReturnsPageWithoutErrors()
        {
            var page = new BloodPressureModel { BP = new BloodPressure { Systolic = 120, Diastolic = 80 } };

            var result = page.OnPost();

            Assert.IsInstanceOfType<PageResult>(result);
            Assert.IsTrue(page.ModelState.IsValid);
        }

        [TestMethod]
        [DataRow(80, 80)]   // equal values
        [DataRow(70, 90)]   // systolic lower than diastolic
        public void OnPost_SystolicNotGreaterThanDiastolic_AddsModelError(int systolic, int diastolic)
        {
            var page = new BloodPressureModel { BP = new BloodPressure { Systolic = systolic, Diastolic = diastolic } };

            page.OnPost();

            Assert.IsFalse(page.ModelState.IsValid);
        }
    }
}

// I GOT HELP FROM CLAUDE FOR THESE TESTS.