using Microsoft.EntityFrameworkCore;

namespace WarehouseProject.Models
{
  public class WarehouseProjectContext : DbContext
  {
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<WarehouseProduct> WarehouseProducts { get; set; }

    public WarehouseProjectContext(DbContextOptions options) : base(options) { }
  }
}