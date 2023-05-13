using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Packages.HttpExceptions;

namespace e_commerce_server.src.Core.Modules.Location
{
    public class LocationRepository 
    {
        private readonly AppDbContext _context;
        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<CityData> GetAllCities()
        {
            try
            {
                return _context.Cities.ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        public List<DistrictData> GetDistrictsByCityId(int cityId)
        {
            try
            {
                return _context.Districts
                    .Where(d => d.city_id == cityId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
