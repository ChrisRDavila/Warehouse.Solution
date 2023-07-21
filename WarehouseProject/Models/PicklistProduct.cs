using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
  public class PicklistProduct
  {
    public int PicklistProductId { get; set; }
    public int PicklistId { get; set; }
    public int ProductId { get; set; }
    public Picklist Picklist { get; set; }
    public Product Product { get; set; }
  }
}