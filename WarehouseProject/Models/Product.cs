using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WarehouseProject.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "You must add your product to a product type. Have you created the corresponding product type yet?")]
    public string ProductTypeId { get; set; } 
    [Required(ErrorMessage = "Please enter a type for the product.")]
    public ProductType ProductType { get; set; }
    [Required(ErrorMessage = "Please enter a barcode for the product.")]
    public string Barcode { get; set; }
    public string Description { get; set; }
    public DateTime DateReceived { get; set; }
    public ApplicationUser User { get; set; }
    // public Boolean Picked { get; set; }
    // public string Bin { get; set; }
    
  }
}