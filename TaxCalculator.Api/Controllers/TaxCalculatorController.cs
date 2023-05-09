using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaxCalculatorController : ControllerBase
{
    private readonly ITaxCalculationService _taxCalculationService;

    public TaxCalculatorController(ITaxCalculationService taxCalculationService)
    {
        _taxCalculationService = taxCalculationService;
    }

    [HttpPost]
    [Route(nameof(CalculateTaxAsync))]
    public async Task<ActionResult<TaxCalculation>> CalculateTaxAsync(TaxCalculation model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var register = await _taxCalculationService.CreateTaxCalculationAsync(model);

            if (register.Id > 0)
                return Ok(register);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaxCalculationAsync(int id)
    {
        var taxCalculations = await _taxCalculationService.GetTaxCalculationAsync(id);

        if (taxCalculations == null) return NotFound();

        return Ok(taxCalculations);
    }

    [HttpGet]
    [Route(nameof(GetAllTaxCalculationsAsync))]
    public async Task<IActionResult> GetAllTaxCalculationsAsync()
    {
        var taxCalculations = await _taxCalculationService.GetTaxCalculationsAsync();

        if (taxCalculations == null) return NotFound();

        return Ok(taxCalculations);
    }
}