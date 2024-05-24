using System.Data.Common;
using System.Reflection;
using masifAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace masifAPI.Data;



public class IncidentContext : DbContext{
   public IncidentContext(DbContextOptions<IncidentContext> options)
        : base(options)
    {
    }

    public DbSet<Incident> Incidents { get; set; } = null!;

 protected override void OnModelCreating(ModelBuilder modelBuilder)
{
   modelBuilder.Entity<Incident>()
    .Property(p => p.ValueIncident)
    .HasComputedColumnSql("[Report.Quantity] * [Incident.ItemPrice]");
}
}



public class ReportContext : DbContext
{
 public ReportContext(DbContextOptions<ReportContext> options)
        : base(options)
    {
    }

    public DbSet<Report> Reports { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);
    }
}


public class FoodItemContext : DbContext
{
    public FoodItemContext(DbContextOptions<FoodItemContext> options)
        : base(options)
    {
    }

    public DbSet<FoodItem> FoodItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
       base.OnModelCreating(builder);
    }
    
}