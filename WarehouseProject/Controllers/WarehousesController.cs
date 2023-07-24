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
  public class WarehousesController : Controller
  {
    private readonly WarehouseProjectContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public WarehousesController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
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
          .Include(warehouse => warehouse.JoinWarehouseProductType)
          .ThenInclude(join => join.ProductType)
          .FirstOrDefault(warehouse => warehouse.WarehouseId == id);
      return View(thisWarehouse);
    }

    public ActionResult Edit(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouses => warehouses.WarehouseId == id);
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
    public ActionResult AddProductType(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouses => warehouses.WarehouseId == id);
      ViewBag.ProductTypeId = new SelectList(_db.ProductTypes, "ProductTypeId", "Name");
      return View(thisWarehouse);
    }

    [HttpPost]
    public ActionResult AddProductType(Warehouse warehouse, int productTypeId)
    {
      #nullable enable
      WarehouseProductType? joinEntity = _db.WarehouseProductTypes.FirstOrDefault(join => (join.ProductTypeId == productTypeId && join.WarehouseId == warehouse.WarehouseId));
      #nullable disable
      if (joinEntity == null && productTypeId != 0)
      {
        _db.WarehouseProductTypes.Add(new WarehouseProductType() { ProductTypeId = productTypeId, WarehouseId = warehouse.WarehouseId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = warehouse.WarehouseId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      WarehouseProductType joinEntry = _db.WarehouseProductTypes.FirstOrDefault(entry => entry.WarehouseProductTypeId == joinId);
      _db.WarehouseProductTypes.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 
  }
}    