using Microsoft.EntityFrameworkCore;

namespace web
{
    public class Database : DbContext{
        public DbSet<Driver> Drivers{set; get;}
        public DbSet<Race> Races{set; get;}
        public DbSet<RaceCar> RaceCars{set; get;}
        public Database(DbContextOptions<Database> options) : base(options) { }

    }
}