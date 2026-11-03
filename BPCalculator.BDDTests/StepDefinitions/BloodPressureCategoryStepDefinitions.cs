using BPCalculator.Pages;

namespace BPCalculator.BDDTests.StepDefinitions
{
    [Binding]
    public sealed class BloodPressureCategoryStepDefinitions
    {
        private readonly BloodPressure _bp = new BloodPressure();
        private readonly BloodPressureModel _page = new BloodPressureModel();
        private BPCategory _category;

        [Given("a systolic pressure of {int} mmHg")]
        public void GivenASystolicPressureOf(int systolic)
        {
            _bp.Systolic = systolic;
        }

        [Given("a diastolic pressure of {int} mmHg")]
        public void GivenADiastolicPressureOf(int diastolic)
        {
            _bp.Diastolic = diastolic;
        }

        [When("the blood pressure category is calculated")]
        public void WhenTheBloodPressureCategoryIsCalculated()
        {
            _category = _bp.Category;
        }

        [When("the reading is submitted")]
        public void WhenTheReadingIsSubmitted()
        {
            _page.BP = _bp;
            _page.OnPost();
        }

        [Then("the category should be {string}")]
        public void ThenTheCategoryShouldBe(string expected)
        {
            Assert.AreEqual(Enum.Parse<BPCategory>(expected), _category);
        }

        [Then("the reading should be rejected")]
        public void ThenTheReadingShouldBeRejected()
        {
            Assert.IsFalse(_page.ModelState.IsValid);
        }
    }
}