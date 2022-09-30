using Microsoft.AspNetCore.Identity;

namespace CoffeeLocator.Repo.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
