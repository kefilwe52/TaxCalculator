using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostalCodeInfoController : ControllerBase
{
    private readonly IPostalCodeInfoService _postalCodeInfoService;

    public PostalCodeInfoController(IPostalCodeInfoService postalCodeInfoService)
    {
        _postalCodeInfoService = postalCodeInfoService;
    }

    [HttpGet]
    public async Task<IEnumerable<PostalCodeInfo>> GetAll()
    {
        return await _postalCodeInfoService.GetAllPostalCodeInfoAsync();
    }
}