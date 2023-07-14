using System.Collections.Generic;
using System;

namespace WarehouseProject.Models
{
  public class Picklist
  {
    public int PicklistId { get; set; }
    public string OrderNumber { get; set; }
    public string OrderFor { get; set; }
    public Boolean Fulfilled { get; set; }
    public string Picker { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public List<Product> Products { get; set; }
    public ApplicationUser User { get; set; }

  }
}