using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WarehouseProject.Models
{
  public class WarehouseProjectContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Picklist> Picklists { get; set; }  
    public DbSet<WarehouseProductType> WarehouseProductTypes { get; set; }
    public DbSet<PicklistProductType> PicklistProductTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public WarehouseProjectContext(DbContextOptions options) : base(options) { }
  }
}