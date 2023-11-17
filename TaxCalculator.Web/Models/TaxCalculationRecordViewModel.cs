using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculator.Web.Models
{
    public class TaxCalculationRecordViewModel
    {
        public int Id { get; set; }
        public decimal Income { get; set; }
        public decimal TaxAmount { get; set; }
        public int PostalCodeTaxTypeId { get; set; }
        public IEnumerable<SelectListItem> PostalCodeTaxTypes { get; set; }
    }
}
