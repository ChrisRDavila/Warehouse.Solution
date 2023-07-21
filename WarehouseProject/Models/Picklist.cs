using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace WarehouseProject.Models
{
  public class Picklist
  {
    public int PicklistId { get; set; }
    [Required(ErrorMessage = "Please enter an order number.")]
    public string OrderNumber { get; set; }
    [Required(ErrorMessage = "Please enter a name for the order.")]
    public string OrderFor { get; set; }
    public Boolean Fulfilled { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public List<Product> Products { get; set; }
    public ApplicationUser User { get; set; }
    public string Carrier { get; set; }
    public DateTime DateOrdered { get; set; }
    public DateTime DateReceived { get; set; }
    public DateTime DateShipped { get; set; }

  }
}