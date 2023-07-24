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
    public ActionResult Edit(Product product)
    {
      _db.Products.Update(product);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      return View(thisProduct);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      _db.Products.Remove(thisProduct);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult AddWarehouse(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(products => products.ProductId == id);
      ViewBag.WarehouseId = new SelectList(_db.Warehouses, "WarehouseId", "WarehouseName");
      return View(thisProduct);
    }

    [HttpPost]
    public ActionResult AddWarehouse(Product product, int warehouseId)
    {
      #nullable enable
      WarehouseProduct? joinEntity = _db.WarehouseProducts.FirstOrDefault(join => (join.WarehouseId == warehouseId && join.ProductId == product.ProductId));
      #nullable disable
      if (joinEntity == null && warehouseId != 0)
      {
        _db.WarehouseProducts.Add(new WarehouseProduct() { WarehouseId = warehouseId, ProductId = product.ProductId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = product.ProductId });
    }   

    [HttpPost]
    public ActionResult DeleteJoinWarehouse(int joinId)
    {
      WarehouseProduct joinEntry = _db.WarehouseProducts.FirstOrDefault(entry => entry.WarehouseId == joinId);
      _db.WarehouseProducts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPicklist(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(products => products.ProductId == id);
      ViewBag.PicklistId = new SelectList(_db.Picklists, "PicklistId", "OrderNumber");
      return View(thisProduct);
    }

    [HttpPost]
    public ActionResult AddPicklist(Product product, int picklistId)
    {
      #nullable enable
      PicklistProduct? joinEntity = _db.PicklistProducts.FirstOrDefault(join => (join.PicklistId == picklistId && join.ProductId == product.ProductId));
      #nullable disable
      if (joinEntity == null && picklistId != 0)
      {
        _db.PicklistProducts.Add(new PicklistProduct() { PicklistId = picklistId, ProductId = product.ProductId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = product.ProductId });
    }

    [HttpPost]
    public ActionResult DeleteJoinPicklist(int joinId)
    {
      PicklistProduct joinEntry = _db.PicklistProducts.FirstOrDefault(entry => entry.PicklistId == joinId);
      _db.PicklistProducts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }  
  }
}   