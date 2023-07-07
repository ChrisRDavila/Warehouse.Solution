namespace WarehouseProject.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string ProductDescription { get; set; }
    public string Barcode { get; set; }
    public int Quantity { get; set; }
    public int Weight { get; set; }
    public int Dimensions { get; set; }
    public int VendorId { get; set; }
    public int WarehouseId { get; set; }
  }
}