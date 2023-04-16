using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Location
{
    public class LocationRepository 
    {
        private readonly MyDbContext _context;
        public LocationRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<object>? GetAllCities()
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

        public List<object>? GetDistrictsByCityId(int cityId)
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
