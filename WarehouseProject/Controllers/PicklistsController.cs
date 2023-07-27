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
                                  .Include(picklist => picklist.JoinPicklistProductType)
          .ThenInclude(join => join.ProductType)
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

    public ActionResult AddProductType(int id)
    {
      Picklist thisPicklist = _db.Picklists.FirstOrDefault(picklists => picklists.PicklistId == id);
      ViewBag.ProductTypeId = new SelectList(_db.ProductTypes, "ProductTypeId", "Name");
      return View(thisPicklist);
    }

    [HttpPost]
    public ActionResult AddProductType(Picklist picklist, int productTypeId)
    {
      #nullable enable
      PicklistProductType? joinEntity = _db.PicklistProductTypes.FirstOrDefault(join => (join.ProductTypeId == productTypeId && join.PicklistId == picklist.PicklistId));
      #nullable disable
      if (joinEntity == null && productTypeId != 0)
      {
        _db.PicklistProductTypes.Add(new PicklistProductType() { ProductTypeId = productTypeId, PicklistId = picklist.PicklistId });
        _db.Picklists.Update(picklist);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = picklist.PicklistId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      PicklistProductType joinEntry = _db.PicklistProductTypes.FirstOrDefault(entry => entry.PicklistProductTypeId == joinId);
      _db.PicklistProductTypes.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 

  }
}   