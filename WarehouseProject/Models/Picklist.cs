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
    public string OrderFor { get; set; }
    public int QuantityNeeded { get; set; }
    public Boolean Fulfilled { get; set; }
    public string Priority { get; set; }
    public string Carrier { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipBy { get; set; }
    public List<PicklistProductType> JoinPicklistProductType { get; set; }
    public ApplicationUser User { get; set; }

  }
}