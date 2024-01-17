using CityInfo.Entities;

namespace CityInfo.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByUserNameAndPassworodAsync(string userName,string password);
}
