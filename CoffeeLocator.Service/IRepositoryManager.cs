namespace CoffeeLocator.Service;

public interface IRepositoryManager
{
    IUserAuthenticationRepository UserAuthentication { get; }
    Task SaveAsync();
}
