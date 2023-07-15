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
  public class ProductsController : Controller
  {
    private readonly WarehouseProjectContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductsController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Product> userProducts = _db.Products
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .Include(product => product.Picklist)
                          .ToList();
      return View(userProducts);
    }

    public ActionResult Create()
    {
      ViewBag.PicklistId = new SelectList(_db.Picklists, "PicklistId", "OrderNumber");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Product product)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.PicklistId = new SelectList(_db.Picklists, "PicklistId", "OrderNumber");
        return View(product);
      }
      else
      {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      product.User = currentUser;
      _db.Products.Add(product);
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Product thisProduct = _db.Products
          .Include(product => product.Picklist)
          .Include(product => product.JoinEntities)
          .ThenInclude(join => join.Warehouse)
          .FirstOrDefault(product => product.ProductId == id);
      return View(thisProduct);
    }

    public ActionResult Edit(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      ViewBag.PicklistId = new SelectList(_db.Picklists, "PicklistId", "OrderNumber");
      return View(thisProduct);
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
    public ActionResult DeleteJoin(int joinId)
    {
      WarehouseProduct joinEntry = _db.WarehouseProducts.FirstOrDefault(entry => entry.WarehouseId == joinId);
      _db.WarehouseProducts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 
  }
}   