using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DbContexts;

public class CityInfoDbContext : DbContext
{

    public CityInfoDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<City> Cities { get; set; }

    public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasData(
            new City() { Id = 1, Name = "London", Description = "London is a very old an beautiful city" },
            new City() { Id = 2, Name = "Paris", Description = "Paris is a very old an beautiful city" },
            new City() { Id = 3, Name = "NewYork", Description = "NewYork is a very old an beautiful city" }
            );

        modelBuilder.Entity<PointOfInterest>()
            .HasData(
            new PointOfInterest { Id = 1, Name = "The old church", Description = "This church belongs to first century", CityId = 1 },
            new PointOfInterest { Id = 2, Name = "The old bridge", Description = "This bridge belongs to first century", CityId = 1 },
            new PointOfInterest { Id = 3, Name = "The old Castle", Description = "This Castle belongs to first century", CityId = 1 }
            );

        modelBuilder.Entity<User>()
            .HasData(
            new User { Id = 1, UserName = "AliReza", Name = "Ali", Family = "Reza", Password = "ThisIsPass" },
            new User { Id = 2, UserName = "Gladiator", Name = "Arian", Family = "Haghighi", Password = "ThisIsPass" },
            new User { Id = 3, UserName = "SinaPro", Name = "Sina", Family = "Chehry", Password = "ThisIsPass" },
            new User { Id = 4, UserName = "Ati", Name = "Atefe", Family = "Asadi", Password = "ThisIsPass" }
            );

        base.OnModelCreating(modelBuilder);
    }
}
