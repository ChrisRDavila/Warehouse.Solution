using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WarehouseProject.Controllers
{
  [Authorize] 
  public class WarehouseController : Controller
  {
    private readonly WarehouseProjectContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public WarehouseController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Warehouse> userWarehouses = _db.Warehouses
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .ToList();
      return View(userWarehouses);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Warehouse warehouse)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      warehouse.User = currentUser;
      _db.Warehouses.Add(warehouse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses
          .Include(warehouse => warehouse.JoinEntities)
          .ThenInclude(join => join.Product)
          .FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }

    public ActionResult Edit(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }

    [HttpPost]
    public ActionResult Edit(Warehouse warehouse)
    {
      _db.Warehouses.Update(warehouse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      _db.Warehouses.Remove(thisWarehouse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddFlavor(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouses => warehouses.WarehouseId == id);
      ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "ProductName");
      return View(thisWarehouse);
    }

    [HttpPost]
    public ActionResult AddProduct(Warehouse warehouse, int productId)
    {
      #nullable enable
      WarehouseProduct? joinEntity = _db.WarehouseProducts.FirstOrDefault(join => (join.ProductId == productId && join.WarehouseId == warehouse.WarehouseId));
      #nullable disable
      if (joinEntity == null && productId != 0)
      {
        _db.WarehouseProducts.Add(new WarehouseProduct() { ProductId = productId, WarehouseId = warehouse.WarehouseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = warehouse.WarehouseId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      WarehouseProduct joinEntry = _db.WarehouseProducts.FirstOrDefault(entry => entry.WarehouseProductId == joinId);
      _db.WarehouseProducts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 
  }
}    