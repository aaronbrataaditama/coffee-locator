namespace CoffeeLocator.Service;

public class RepositoryManager : IRepositoryManager
{
    private RepositoryContext _repositoryContext;

    private IUserAuthenticationRepository _userAuthenticationRepository;
    private UserManager<User> _userManager;
    private IMapper _mapper;
    private IConfiguration _configuration;

    public RepositoryManager(RepositoryContext repositoryContext, UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
    {
        _repositoryContext = repositoryContext;
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    public IUserAuthenticationRepository UserAuthentication
    {
        get
        {
            if (_userAuthenticationRepository is null)
                _userAuthenticationRepository = new UserAuthenticationRepository(_userManager, _configuration, _mapper);
            return _userAuthenticationRepository;
        }
    }

    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

}
