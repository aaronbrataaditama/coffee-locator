namespace CoffeeLocator.Controllers;

[Route("api/userauthentication")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly ILogger<AuthController> _logger;
    public AuthController(IRepositoryManager repository, ILogger<AuthController> logger, IMapper mapper) : base(repository, mapper) => _logger = logger;

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
    {

        var userResult = await _repository.UserAuthentication.RegisterUserAsync(userRegistration);
        return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
    {
        return !await _repository.UserAuthentication.ValidateUserAsync(user)
            ? Unauthorized()
            : Ok(new { Token = await _repository.UserAuthentication.CreateTokenAsync() });
    }
}
