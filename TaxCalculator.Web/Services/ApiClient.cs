using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TaxCalculator.Web.Models;

namespace TaxCalculator.Web.Services
{
    public class ApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<SelectListItem>> GetPostalCodeTaxTypesAsync()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://localhost:7039/api/TaxCalculatorRecord/PostalCodeTaxTypes");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var taxTypes = JsonConvert.DeserializeObject<IEnumerable<PostalCodeTaxTypeViewModel>>(jsonString);
                return taxTypes.Select(tt => new SelectListItem { Value = tt.Id.ToString(), Text = tt.TaxCalculationType });
            }
            catch (HttpRequestException ex)
            {
                return new List<SelectListItem> { };
            }
        }

        public async Task<TaxCalculationRecordViewModel> AddTaxCalculationRecordAsync(TaxCalculationRecordViewModel recordModel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(recordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:7039/api/TaxCalculatorRecord/AddTaxCalculationRecord", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var record = JsonConvert.DeserializeObject<TaxCalculationRecordViewModel>(responseContent);
                return record;
            }

            return null;
        }
    }
}
