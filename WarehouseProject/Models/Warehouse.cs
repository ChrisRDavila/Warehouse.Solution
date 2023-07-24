using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WarehouseProject.Models
{
  public class Warehouse
  {
    public int WarehouseId { get; set; }
    [Required(ErrorMessage = "Please enter a unique identifying name for this building.")]
    public string Branch { get; set; }
    [Required(ErrorMessage = "Please enter an address for the warehouse.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Please enter a city for the warehouse.")]
    public string City { get; set; }
    [Required(ErrorMessage = "Please enter a state for the warehouse.")]
    public string State { get; set; }
    [Range(10000, 99999, ErrorMessage = "Please enter a valid zipcode.")]
    public int Zipcode { get; set; }
    public string Notes { get; set; }
    public List<WarehouseProductType> JoinWarehouseProductType { get; }
    public ApplicationUser User { get; set; }
    
  }
}