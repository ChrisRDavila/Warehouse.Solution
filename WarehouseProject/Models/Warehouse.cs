using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WarehouseProject.Models
{
  public class Warehouse
  {
    public int WarehouseId { get; set; }
    [Required(ErrorMessage = "Please enter a name for the warehouse.")]
    public string WarehouseName { get; set; }
    [Required(ErrorMessage = "Please enter an address for the warehouse.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Please enter a city for the warehouse.")]
    public string City { get; set; }
    [Required(ErrorMessage = "Please enter a state for the warehouse.")]
    public string State { get; set; }
    [Required(ErrorMessage = "Please enter a zipcode for the warehouse.")]
    public string Zipcode { get; set; }
    [Required(ErrorMessage = "Please enter a phone number for the warehouse desciption.")]
    public string WHDescription { get; set; }
    public List<WarehouseProduct> JoinEntities { get; set; }
  }
}