using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WarehouseProject.Models
{
  public class ProductType
  {
    public int ProductTypeId { get; set; }
    [Required(ErrorMessage = "Please enter a name for the product.")]
    public string Name { get; set; } 
    public string Model { get; set;}
    public string Description { get; set; }
    public int Quantity { get; set; }
    // [Range(0, int.MaxValue, ErrorMessage = "You must add positive number in lbs to weight")]
    public int ShipUnit { get; set; }
    public decimal Weight { get; set; }
    
    // [Required(ErrorMessage = "Please enter a dimension for lxwxh in inches for the product.")]
    public string Dimensions { get; set; }
    public List<PicklistProductType> JoinPicklistProductType { get; set; }
    public List<WarehouseProductType> JoinWarehouseProductType { get; }
    public List<Product> Products { get; set; }
    public ApplicationUser User { get; set; }
    
  }
}