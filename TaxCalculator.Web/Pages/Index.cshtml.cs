using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public IndexModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public IList<TaxCalculation> TaxCalculation { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var client = _clientFactory.CreateClient("api");
        var response = await client.GetAsync("TaxCalculator/GetAllTaxCalculationsAsync");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            TaxCalculation = JsonConvert.DeserializeObject<List<TaxCalculation>>(json);
        }
    }
}