namespace CoffeeLocator.Services;

public interface ICoffeeLocationService
{
    Task<IEnumerable<NearByResult>> GetNearbyCoffeePlaces(double latitude, double longitude, int radius = 5000);
}