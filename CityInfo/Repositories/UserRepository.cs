using CityInfo.DbContexts;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CityInfoDbContext _context;

    public UserRepository(CityInfoDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUserNameAndPassworodAsync(string userName, string password) =>
        await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
}
