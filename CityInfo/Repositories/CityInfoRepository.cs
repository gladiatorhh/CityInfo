using CityInfo.DbContexts;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext _context;

        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context;
        }

        public async Task AddPointOfInterestToCity(int cityId, PointOfInterest pointOfInterest)
        {
            City city = await GetCityByIdAsync(cityId);

            city.PointsOfInterest.Add(pointOfInterest);
        }

        public async Task<bool> CheckCityExistByCityIdAsync(int cityId) =>
            await _context.Cities.AnyAsync(c => c.Id == cityId);

        public void DeletePointOfInterestById(PointOfInterest pointOfInterest) =>
            _context.PointsOfInterest.Remove(pointOfInterest);

        public async Task<IEnumerable<City>> GetCitiesAsync(bool includePointsOfInterest = false)
        {
            IQueryable<City> cities = _context.Cities;

            if (includePointsOfInterest)
                cities = cities.Include(c => c.PointsOfInterest);

            return await cities.ToListAsync();
        }

        public async Task<City?> GetCityByIdAsync(int cityId, bool includePointsOfInterest = false)
        {
            IQueryable<City> cities = _context.Cities;

            if (includePointsOfInterest)
                cities = cities.Include(c => c.PointsOfInterest);

            return await cities.FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestByIdAsync(int cityId, int pointId) =>
            await _context.PointsOfInterest.FirstOrDefaultAsync(p => cityId == cityId && p.Id == pointId);

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestByCityAsync(int cityId, bool includeCity = false)
        {
            IQueryable<PointOfInterest> points = _context.PointsOfInterest;

            if (includeCity)
            {
                points = points.Include(p => p.City);
            }

            return await points.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
