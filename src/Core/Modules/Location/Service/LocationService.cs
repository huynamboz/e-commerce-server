using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Utils;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Location.Service
{
    public class LocationService
    {
        private LocationRepository cityRepository;
        public LocationService(AppDbContext context)
        {
            cityRepository = new LocationRepository(context);
        }
        public object? GetAllCities()
        {
            return cityRepository.GetAllCities();
        }

        public object? GetDistrictsByCityId(int cityId)
        {
            var districts = cityRepository.GetDistrictsByCityId(cityId);

            Optional.Of(districts).ThrowIfNotPresent(new BadRequestException(LocationEnum.LOCATION_NOT_FOUND));

            return districts;
        }

    }
}
