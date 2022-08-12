using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SpecificationPattern.Common.Models;

namespace SpecificationPattern.Common
{
    public class SpecificationPatternDbContext : DbContext
    {
        public SpecificationPatternDbContext(DbContextOptions<SpecificationPatternDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>()
                .HasKey(w => w.Id);
            modelBuilder.Entity<WeatherForecast>()
                .HasData(new WeatherForecast()
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(1),
                        Summary = "Cold",
                        TemperatureC = -20
                    },
                    new WeatherForecast()
                    {
                        Id = 2,
                        Date = DateTime.Now.AddDays(2),
                        Summary = "Cold",
                        TemperatureC = 0
                    },
                    new WeatherForecast()
                    {
                        Id = 3,
                        Date = DateTime.Now.AddDays(3),
                        Summary = "Cold",
                        TemperatureC = 20
                    },
                    new WeatherForecast()
                    {
                        Id = 4,
                        Date = DateTime.Now.AddDays(4),
                        Summary = "Normal",
                        TemperatureC = 40
                    },
                    new WeatherForecast()
                    {
                        Id = 5,
                        Date = DateTime.Now.AddDays(5),
                        Summary = "Normal",
                        TemperatureC = 60
                    },
                    new WeatherForecast()
                    {
                        Id = 6,
                        Date = DateTime.Now.AddDays(6),
                        Summary = "Warm",
                        TemperatureC = 80
                    },
                    new WeatherForecast()
                    {
                        Id = 7,
                        Date = DateTime.Now.AddDays(7),
                        Summary = "Warm",
                        TemperatureC = 100
                    },
                    new WeatherForecast()
                    {
                        Id = 8,
                        Date = DateTime.Now.AddDays(8),
                        Summary = "Hot",
                        TemperatureC = 120
                    },
                    new WeatherForecast()
                    {
                        Id = 9,
                        Date = DateTime.Now.AddDays(9),
                        Summary = "Hot",
                        TemperatureC = 140
                    },
                    new WeatherForecast()
                    {
                        Id = 10,
                        Date = DateTime.Now.AddDays(10),
                        Summary = "Hot",
                        TemperatureC = 160
                    });
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SpecificationPatternDbContext>
        {
            public SpecificationPatternDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<SpecificationPatternDbContext>();
                builder.UseInMemoryDatabase("SpecificationPatternApi");
                return new SpecificationPatternDbContext(builder.Options);
            }
        }
    }
}