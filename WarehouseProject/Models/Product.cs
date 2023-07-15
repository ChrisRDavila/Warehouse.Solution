using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WarehouseProject.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Please enter a name for the product.")]
    public string Name { get; set; } 
    [Required(ErrorMessage = "Please enter a type for the product.")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Please enter a description for the product.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Please enter a barcode for the product.")]
    public string Barcode { get; set; }
    // [Range(0, int.MaxValue, ErrorMessage = "You must add your amount to quantity")]
    public int Quantity { get; set; }
    // [Range(0, int.MaxValue, ErrorMessage = "You must add positive number in lbs to weight")]
    public decimal Weight { get; set; }
    
    // [Required(ErrorMessage = "Please enter a dimension for lxwxh in inches for the product.")]
    public string Dimensions { get; set; }
    // public int VendorId { get; set; }
    // public int WarehouseId { get; set; }
    public DateTime DateOrdered { get; set; }
    public DateTime DateReceived { get; set; }
    public DateTime DateShipped { get; set; }
    public int PicklistId { get; set; }
    public Picklist Picklist { get; set; }
    public List<WarehouseProduct> JoinEntities { get; }
    public ApplicationUser User { get; set; }
    public Boolean Picked { get; set; }
  }
}