using System.Data.Common;
using System.Reflection;
using masifAPI.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace masifAPI.Data;



public class MasifContext : DbContext{
   public MasifContext(DbContextOptions<MasifContext> options)
        : base(options)
    {
    }

    public DbSet<Incident> Incidents { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<FoodItem> FoodItems { get; set; } = null!;
    public DbSet<Picture> Pictures { get; set; } = null!;

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=35.226.45.13;Database=masifAPI;Username=knutekje;Password=hore23");
    }
public class MasifContextFactory : IDesignTimeDbContextFactory<MasifContext>
{
    public MasifContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MasifContext>();
        optionsBuilder.UseNpgsql("Host=35.226.45.13;Database=masifAPI;Username=knutekje;Password=hore23");

        return new MasifContext(optionsBuilder.Options);
    }
}











