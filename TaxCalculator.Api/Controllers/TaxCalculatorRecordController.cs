using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Api.Models;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorRecordController : ControllerBase
    {
        private readonly ITaxCalculationRecordService _taxCalculationRecordService;
        private readonly IPostalCodeTaxTypeService _postalCodeTaxTypeService;

        public TaxCalculatorRecordController(ITaxCalculationRecordService taxCalculationRecordService,
            IPostalCodeTaxTypeService postalCodeTaxTypeService)
        {
            _taxCalculationRecordService = taxCalculationRecordService;
            _postalCodeTaxTypeService = postalCodeTaxTypeService;
        }

        [HttpGet("TaxCalculationRecords")]
        public async Task<IActionResult> GetTaxCalculationRecords()
        {
            return Ok(await _taxCalculationRecordService.GetTaxCalculationRecords());
        }

        [HttpGet("PostalCodeTaxTypes")]
        public async Task<IActionResult> GetPostalCodeTaxTypes()
        {
            return Ok(await _postalCodeTaxTypeService.PostalCodeTaxTypes());
        }

        [HttpPost("AddTaxCalculationRecord")]
        public async Task<IActionResult> AddTaxCalculationRecord([FromBody] TaxCalculationRecordModel recordDto)
        {
            if (recordDto == null)
                return BadRequest("Tax calculation record data is required.");

            var record = new TaxCalculationRecord
            {
                Income = recordDto.Income,
                TaxAmount = recordDto.TaxAmount,
                PostalCodeTaxTypeId = recordDto.PostalCodeTaxTypeId
            };

            var results = await _taxCalculationRecordService.AddTaxCalculationRecord(record);

            if (results.Id > 0)
                return Ok(results);

            return BadRequest();
        }
    }
}
