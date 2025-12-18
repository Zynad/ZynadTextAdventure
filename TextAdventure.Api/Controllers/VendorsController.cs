using ApplicationServices.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace TextAdventure.Api.Controllers;

[ApiController]
[Route("api/vendors")]
public class VendorsController(IVendorPricingService vendorPricingService) : ControllerBase
{
    /// <summary>
    /// Get vendor prices for a given town.
    /// </summary>
    /// <param name="townName">The town to query.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet("{townName}/prices")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPrices(string townName, CancellationToken cancellationToken)
    {
        var prices = await vendorPricingService.GetPricesAsync(townName, cancellationToken);
        return Ok(new
        {
            town = townName,
            prices
        });
    }
}
