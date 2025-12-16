using ApplicationServices.Contracts.Services;
using Microsoft.AspNetCore.Http;
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

    /// <summary>
    /// Get vendor prices for a given town.
    /// </summary>
    /// <param name="townName">The town to query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("{townName}/prices")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
