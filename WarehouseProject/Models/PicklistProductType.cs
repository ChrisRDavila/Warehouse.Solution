using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
  public class PicklistProductType
  {
    public int PicklistProductTypeId { get; set; }
    public int PicklistId { get; set; }
    public int ProductTypeId { get; set; }
    public Picklist Picklist { get; set; }
    public ProductType ProductType { get; set; }
  }
}