namespace CoffeeLocator.Repo.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile() => CreateMap<UserRegistrationDto, User>();
}
