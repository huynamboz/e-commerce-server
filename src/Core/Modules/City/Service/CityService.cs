using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Utils;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.City.Service
{
    public class CityService
    {
        private CityRepository cityRepository;
        public CityService(MyDbContext context)
        {
            cityRepository = new CityRepository(context);
        }
        public object? GetAllCities()
        {
            return cityRepository.GetAllCities();
        }

        public object GetDistrictsByCityId(int cityid)
        {
            var districts = cityRepository.GetDistrictsByCityId(cityid);

            Optional.Of(districts).ThrowIfNotPresent(new BadRequestException("city id not found"));

            return districts;
        }

    }
}
