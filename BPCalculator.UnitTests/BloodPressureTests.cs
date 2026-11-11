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

        // MAP = Diastolic + (Systolic - Diastolic) / 3, rounded to a whole number
        [TestMethod]
        [DataRow(70, 40, 50)]    // minimum values
        [DataRow(90, 60, 70)]    // whole number result
        [DataRow(120, 80, 93)]   // 93.33 is rounded down
        [DataRow(150, 100, 117)] // 116.67 is rounded up
        [DataRow(190, 100, 130)] // maximum values
        public void MeanArterialPressure_ReturnsExpectedValue(int systolic, int diastolic, int expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            Assert.AreEqual(expected, bp.MAP);
        }

    }
}