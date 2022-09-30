namespace CoffeeLocator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoffeeLocationController : ControllerBase
{
    private readonly ILogger<CoffeeLocationController> _logger;
    private ICoffeeLocationService _coffeeLocationService;

    public CoffeeLocationController(ILogger<CoffeeLocationController> logger, ICoffeeLocationService coffeeLocationService)
    {
        _logger = logger;
        _coffeeLocationService = coffeeLocationService;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NearByResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNearbyCoffee([FromQuery]double longitude, [FromQuery] double latitude, [FromQuery] int radius = 1000)
    {
        try
        {
            IEnumerable<NearByResult> nearbyPlaces = await _coffeeLocationService.GetNearbyCoffeePlaces(latitude, longitude, radius);
            if (nearbyPlaces.Count() > 0)
            {
                return Ok(nearbyPlaces);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetNearbyCoffee():{0}", ex.Message);
        }

        return NotFound();
    }
}

