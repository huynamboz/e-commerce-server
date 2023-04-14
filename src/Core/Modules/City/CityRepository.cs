using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.City
{
    public class CityRepository 
    {
        private readonly MyDbContext _context;
        public CityRepository(MyDbContext context)
        {
            _context = context;
        }
        public object? GetAllCities()
        {
            try
            {
                return _context.Cities
                    .Select(city => new
                    {
                        city.id,
                        city.name,
                    }).Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public object GetDistrictsByCityId(int cityId)
        {
            try
            {
                return _context.Districts
                    .Where(d => d.city_id == cityId)
                    .Select(district => new
                    {
                        district.id,
                        district.name,
                    }).Cast<object>().ToList();

            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
