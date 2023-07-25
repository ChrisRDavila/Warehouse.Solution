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
  public class ProductTypesController : Controller
  {
    private readonly WarehouseProjectContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductTypesController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<ProductType> userProductTypes = _db.ProductTypes
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .ToList();
      return View(userProductTypes);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductType productType)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      productType.User = currentUser;
      _db.ProductTypes.Add(productType);
      _db.SaveChanges();
      return RedirectToAction("Index");
      
    }

    public ActionResult Details(int id)
    {
      ProductType thisProductType = _db.ProductTypes
          .Include(productType => productType.Products)
          .Include(productType => productType.JoinPicklistProductType)
          .ThenInclude(join => join.Picklist)
          .Include(productType => productType.JoinWarehouseProductType)
          .ThenInclude(join => join.Warehouse)
          .FirstOrDefault(productType => productType.ProductTypeId == id);
      return View(thisProductType);
    }

    public ActionResult Edit(int id)
    {
      ProductType thisProductType = _db.ProductTypes.FirstOrDefault(productType => productType.ProductTypeId == id);
      return View(thisProductType);
    }

    [HttpPost]
    public ActionResult Edit(ProductType productType)
    {
      _db.ProductTypes.Update(productType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      ProductType thisProductType = _db.ProductTypes.FirstOrDefault(productType => productType.ProductTypeId == id);
      return View(thisProductType);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      ProductType thisProductType = _db.ProductTypes.FirstOrDefault(productType => productType.ProductTypeId == id);
      _db.ProductTypes.Remove(thisProductType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddWarehouse(int id)
    {
      ProductType thisProductType = _db.ProductTypes.FirstOrDefault(productTypes => productTypes.ProductTypeId == id);
      ViewBag.WarehouseId = new SelectList(_db.Warehouses, "WarehouseId", "WarehouseName");
      return View(thisProductType);
    }

    [HttpPost]
    public ActionResult AddWarehouse(ProductType productType, int warehouseId)
    {
      #nullable enable
      WarehouseProductType? joinEntity = _db.WarehouseProductTypes.FirstOrDefault(join => (join.WarehouseId == warehouseId && join.ProductTypeId == productType.ProductTypeId));
      #nullable disable
      if (joinEntity == null && warehouseId != 0)
      {
        _db.WarehouseProductTypes.Add(new WarehouseProductType() { WarehouseId = warehouseId, ProductTypeId = productType.ProductTypeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = productType.ProductTypeId });
    }   

    [HttpPost]
    public ActionResult DeleteJoinWarehouse(int joinId)
    {
      WarehouseProductType joinEntry = _db.WarehouseProductTypes.FirstOrDefault(entry => entry.WarehouseId == joinId);
      _db.WarehouseProductTypes.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPicklist(int id)
    {
      ProductType thisProductType = _db.ProductTypes.FirstOrDefault(productTypes => productTypes.ProductTypeId == id);
      ViewBag.PicklistId = new SelectList(_db.Picklists, "PicklistId", "OrderNumber");
      return View(thisProductType);
    }

    [HttpPost]
    public ActionResult AddPicklist(ProductType productType, int picklistId)
    {
      #nullable enable
      PicklistProductType? joinEntity = _db.PicklistProductTypes.FirstOrDefault(join => (join.PicklistId == picklistId && join.ProductTypeId == productType.ProductTypeId));
      #nullable disable
      if (joinEntity == null && picklistId != 0)
      {
        _db.PicklistProductTypes.Add(new PicklistProductType() { PicklistId = picklistId, ProductTypeId = productType.ProductTypeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = productType.ProductTypeId });
    }

    [HttpPost]
    public ActionResult DeleteJoinPicklist(int joinId)
    {
      PicklistProductType joinEntry = _db.PicklistProductTypes.FirstOrDefault(entry => entry.PicklistId == joinId);
      _db.PicklistProductTypes.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }  
  }
}   