using BPCalculator.Pages;
using Microsoft.Extensions.Logging.Abstractions;

namespace BPCalculator.UnitTests
{
    [TestClass]
    public sealed class PrivacyPageModelTests
    {
        [TestMethod]
        public void OnGet_LoadsPageWithoutErrors()
        {
            var page = new PrivacyModel(NullLogger<PrivacyModel>.Instance);

            page.OnGet();

            Assert.IsTrue(page.ModelState.IsValid);
        }
    }
}