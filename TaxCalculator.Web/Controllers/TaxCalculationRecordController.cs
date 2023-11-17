using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Web.Models;
using TaxCalculator.Web.Services;

namespace TaxCalculator.Web.Controllers
{
    public class TaxCalculationRecordController : Controller
    {
        private readonly ApiClient _apiClient;

        public TaxCalculationRecordController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaxCalculationRecordViewModel
            {
                PostalCodeTaxTypes = await _apiClient.GetPostalCodeTaxTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaxCalculationRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var record = await _apiClient.AddTaxCalculationRecordAsync(new TaxCalculationRecordViewModel
                {
                    Income = model.Income,
                    PostalCodeTaxTypeId = model.PostalCodeTaxTypeId
                });

                if (record != null)
                {
                    return View("TaxCalculationResult", record); // Display the result with the returned record
                }
            }

            model.PostalCodeTaxTypes = await _apiClient.GetPostalCodeTaxTypesAsync();
            return View(model);
        }
    }
}
