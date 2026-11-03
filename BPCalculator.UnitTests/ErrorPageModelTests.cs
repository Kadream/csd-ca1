using BPCalculator.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Abstractions;

namespace BPCalculator.UnitTests
{
    [TestClass]
    public sealed class ErrorPageModelTests
    {
        [TestMethod]
        public void OnGet_SetsRequestId_SoItIsShown()
        {
            var page = new ErrorModel(NullLogger<ErrorModel>.Instance)
            {
                PageContext = new PageContext { HttpContext = new DefaultHttpContext() }
            };

            page.OnGet();

            Assert.IsTrue(page.ShowRequestId);
        }
    }
}