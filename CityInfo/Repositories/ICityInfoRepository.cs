using CityInfo.Entities;

namespace CityInfo.Repositories;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetCitiesAsync(bool includePointsOfInterest = false);

    Task<City?> GetCityByIdAsync(int cityId,bool includePointsOfInterest = false);

    Task<bool> CheckCityExistByCityIdAsync(int cityId);

    Task<IEnumerable<PointOfInterest>> GetPointsOfInterestByCityAsync(int cityId,bool includeCity = false);

    Task<PointOfInterest?> GetPointOfInterestByIdAsync(int cityId,int pointId);

    Task AddPointOfInterestToCity(int cityId,PointOfInterest pointOfInterest);

    void DeletePointOfInterestById(PointOfInterest pointOfInterest);

    Task<bool> SaveChangesAsync();
}
