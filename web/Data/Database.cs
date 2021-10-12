using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace web
{
    public class Database : DbContext{
        public DbSet<Driver> Drivers {set; get;}
        public DbSet<Race> Trials {set; get;}
        public DbSet<RaceCar> RaceCars {set; get;}
        public Database(DbContextOptions<Database> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Driver>()
            .HasMany(p => p.Races)
            .WithMany(c => c.Drivers)
            .UsingEntity<DriverRace>(
                // the functions below need to be in a specific order
                j => j.HasOne(d => d.Race).WithMany(c => c.DriverRace),
                j => j.HasOne(d => d.Driver).WithMany(p => p.DriverRace)
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            // to disable change tracking
            // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            // db.Entry(some_obj).State = EntityState.Detached;

            // use either option below to log the queries to the console
            options.LogTo(Console.WriteLine, LogLevel.Debug);
            // options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

            // displays parameter values in logs
            options.EnableSensitiveDataLogging();
        }

    }
}