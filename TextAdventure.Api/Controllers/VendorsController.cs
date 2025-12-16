using ApplicationServices.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/vendors")]
public class VendorsController : ControllerBase
{
    private readonly IVendorPricingService _vendorPricingService;

    public VendorsController(IVendorPricingService vendorPricingService)
    {
        _vendorPricingService = vendorPricingService;
    }

    [HttpGet("{townName}/prices")]
    public async Task<IActionResult> GetPrices(string townName, CancellationToken cancellationToken)
    {
        var prices = await _vendorPricingService.GetPricesAsync(townName, cancellationToken);
        return Ok(new
        {
            town = townName,
            prices
        });
    }
}
