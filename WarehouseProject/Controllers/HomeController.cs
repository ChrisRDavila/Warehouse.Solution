using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WarehouseProject.Controllers
{
    public class HomeController : Controller
    {
      private readonly WarehouseProjectContext _db;
      private readonly UserManager<ApplicationUser> _userManager;

      public HomeController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
      {
        _userManager = userManager;
        _db = db;
      }

      [HttpGet("/")]
      public async Task<ActionResult> Index()
      {
        Dictionary<string,object[]> model = new Dictionary<string, object[]>();
        string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        if (currentUser != null)
        {
          Product[] products = _db.Products
                      .Where(entry => entry.User.Id == currentUser.Id)
                      .ToArray();
          model.Add("products", products);
          Warehouse[] warehouses = _db.Warehouses
                      .Where(entry => entry.User.Id == currentUser.Id)
                      .ToArray();
          model.Add("warehouses", warehouses);
        }
        return View(model);
      }
    }
}