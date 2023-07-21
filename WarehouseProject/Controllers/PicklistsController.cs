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
  public class PicklistsController : Controller
  {
    private readonly WarehouseProjectContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PicklistsController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Picklist> userPicklists = _db.Picklists
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .ToList();
      return View(userPicklists);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Picklist picklist)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      picklist.User = currentUser;
      _db.Picklists.Add(picklist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Picklist thisPicklist = _db.Picklists
                                  .Include(picklist => picklist.JoinPicklistProduct)
          .ThenInclude(join => join.Product)
          .FirstOrDefault(picklist => picklist.PicklistId == id);
      return View(thisPicklist);
    }

    public ActionResult Edit(int id)
    {
      Picklist thisPicklist = _db.Picklists.FirstOrDefault(picklist => picklist.PicklistId == id);
      return View(thisPicklist);
    }

    [HttpPost]
    public ActionResult Edit(Picklist picklist)
    {
      _db.Picklists.Update(picklist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Picklist thisPicklist = _db.Picklists.FirstOrDefault(picklist => picklist.PicklistId == id);
      return View(thisPicklist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Picklist thisPicklist = _db.Picklists.FirstOrDefault(picklist => picklist.PicklistId == id);
      _db.Picklists.Remove(thisPicklist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddProduct(int id)
    {
      Warehouse thisWarehouse = _db.Warehouses.FirstOrDefault(warehouses => warehouses.WarehouseId == id);
      ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
      return View(thisWarehouse);
    }

    [HttpPost]
    public ActionResult AddProduct(Picklist picklist, int productId)
    {
      #nullable enable
      PicklistProduct? joinEntity = _db.PicklistProducts.FirstOrDefault(join => (join.ProductId == productId && join.PicklistId == picklist.PicklistId));
      #nullable disable
      if (joinEntity == null && productId != 0)
      {
        _db.PicklistProducts.Add(new PicklistProduct() { ProductId = productId, PicklistId = picklist.PicklistId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = picklist.PicklistId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      PicklistProduct joinEntry = _db.PicklistProducts.FirstOrDefault(entry => entry.PicklistProductId == joinId);
      _db.PicklistProducts.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 

  }
}   