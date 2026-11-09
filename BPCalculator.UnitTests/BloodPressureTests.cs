namespace BPCalculator.UnitTests
{
    [TestClass]
    public sealed class BloodPressureTests
    {
        // Boundary values from the BP chart (lower limits are inclusive)
        [TestMethod]
        [DataRow(70, 40, BPCategory.Low)]      // minimum values
        [DataRow(89, 59, BPCategory.Low)]      // just below Ideal
        [DataRow(90, 40, BPCategory.Ideal)]    // systolic lower limit of Ideal
        [DataRow(89, 60, BPCategory.Ideal)]    // diastolic lower limit of Ideal
        [DataRow(119, 79, BPCategory.Ideal)]   // just below Pre-High
        [DataRow(120, 40, BPCategory.PreHigh)] // systolic lower limit of Pre-High
        [DataRow(119, 80, BPCategory.PreHigh)] // diastolic lower limit of Pre-High
        [DataRow(139, 89, BPCategory.PreHigh)] // just below High
        [DataRow(140, 40, BPCategory.High)]    // systolic lower limit of High
        [DataRow(139, 90, BPCategory.High)]    // diastolic lower limit of High
        [DataRow(190, 100, BPCategory.High)]   // maximum values
        public void Category_ReturnsExpectedCategory(int systolic, int diastolic, BPCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            Assert.AreEqual(expected, bp.Category);
        }
    }
}