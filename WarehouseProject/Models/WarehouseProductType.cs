namespace WarehouseProject.Models
{
  public class WarehouseProductType
  {
    public int WarehouseProductTypeId { get; set; }
    public int WarehouseId { get; set; }
    public int ProductTypeId { get; set; }
    public Warehouse Warehouse { get; set; }
    public ProductType ProductType { get; set; }
  }
}