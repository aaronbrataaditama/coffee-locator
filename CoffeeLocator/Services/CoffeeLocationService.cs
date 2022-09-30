namespace CoffeeLocator.Services;

public class CoffeeLocationService : ICoffeeLocationService
{
    private const string COFFEE_CATEGORY = "coffee";

    private readonly IConfiguration _configuration;
    private static GoogleApi.GooglePlaces.Search.NearBySearchApi _nearBySearchApi;

    public CoffeeLocationService(IConfiguration configuration, GoogleApi.GooglePlaces.Search.NearBySearchApi nearBySearchApi)
    {
        _nearBySearchApi = nearBySearchApi;
        _configuration = configuration;
    }

    public async Task<IEnumerable<NearByResult>> GetNearbyCoffeePlaces(double latitude, double longitude, int radius = 1000)
    {
        string googleApiKey = _configuration.GetValue<string>("GoogleApiKey");
        PlacesNearBySearchRequest request = new PlacesNearBySearchRequest()
        {
            Key = googleApiKey,
            Name = COFFEE_CATEGORY,
            Location = new GoogleApi.Entities.Common.Coordinate(longitude, latitude),
            Radius = radius
        };

        try
        {
            List<NearByResult> nearByResults = new List<NearByResult>();

            var response = await _nearBySearchApi.QueryAsync(request);

            if (response.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
            {
                nearByResults.AddRange(response.Results.ToList());

                while (!string.IsNullOrWhiteSpace(response.NextPageToken))
                {
                    request = new PlacesNearBySearchRequest()
                    {
                        Key = googleApiKey,
                        PageToken = response.NextPageToken
                    };

                    response = await _nearBySearchApi.QueryAsync(request);

                    if (response.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
                    {
                        nearByResults.AddRange(response.Results.ToList());
                    }
                }

                return nearByResults;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return new List<NearByResult>();
    }
}
