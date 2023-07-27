using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
  public class PicklistProductType
  {
    public int PicklistProductTypeId { get; set; }
    public bool Fulfilled { get; set; }
    public int QuantityNeeded { get; set; }
    public int PicklistId { get; set; }
    public int ProductTypeId { get; set; }
    public Picklist Picklist { get; set; }
    public ProductType ProductType { get; set; }
    public ApplicationUser User { get; set; }
    public List<PicklistProductType> JoinPicklistProductType { get; set; }
  }
}