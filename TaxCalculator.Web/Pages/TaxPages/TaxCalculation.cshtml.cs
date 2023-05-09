using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Web.Pages.TaxPages;

public class TaxCalculationModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public TaxCalculationModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public List<PostalCodeInfo> PostalCodeInfos { get; set; } = new();

    [BindProperty] public TaxCalculation TaxCalculation { get; set; } = default!;

    [BindProperty] public int SelectedPostalCodeId { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var client = _clientFactory.CreateClient("api");
        var response = await client.GetAsync("PostalCodeInfo");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            PostalCodeInfos = JsonConvert.DeserializeObject<List<PostalCodeInfo>>(json);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(TaxCalculation taxCalculation)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        taxCalculation.PostalCodeInfoId = SelectedPostalCodeId;

        var content = new StringContent(JsonConvert.SerializeObject(taxCalculation), Encoding.UTF8, "application/json");

        var client = _clientFactory.CreateClient("api");
        var response = await client.PostAsync("TaxCalculator/CalculateTaxAsync", content);

        if (!response.IsSuccessStatusCode) return BadRequest($"Failed to calculate tax: {response.ReasonPhrase}");

        return RedirectToPage("/Index");
    }
}