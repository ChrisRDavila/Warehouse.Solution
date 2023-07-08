using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
  public class Vendor
  {
    public int VendorId { get; set; }
    public string VendorName { get; set; }
    public string VendorType { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<WarehouseProduct> WarehouseProducts { get; }
    

  }
}