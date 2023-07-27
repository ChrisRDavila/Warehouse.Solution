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
  public class PicklistProductTypesController : Controller
  {
    private readonly WarehouseProjectContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PicklistProductTypesController(UserManager<ApplicationUser> userManager, WarehouseProjectContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<PicklistProductType> userPicklistProductTypes = _db.PicklistProductTypes
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .ToList();
      return View(userPicklistProductTypes);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(PicklistProductType picklistProductType)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      picklistProductType.User = currentUser;
      _db.PicklistProductTypes.Add(picklistProductType);
      _db.SaveChanges();
      return RedirectToAction("Index");
      
    }

    public ActionResult Details(int id)
    {
      PicklistProductType thisPicklistProductType = _db.PicklistProductTypes
          .Include(PicklistProductType => PicklistProductType.JoinPicklistProductType)
          .Include(picklistProductType => picklistProductType.Picklist)
          .Include(picklistProductType => picklistProductType.ProductType)
          .FirstOrDefault(picklistProductType => picklistProductType.PicklistProductTypeId == id);
      return View(thisPicklistProductType);
    }

    public ActionResult Edit(int id)
    {
      PicklistProductType thisPicklistProductType = _db.PicklistProductTypes.FirstOrDefault(picklistProductType => picklistProductType.PicklistProductTypeId == id);
      return View(thisPicklistProductType);
    }

    [HttpPost]
    public ActionResult Edit(PicklistProductType picklistProductType)
    {
      _db.PicklistProductTypes.Update(picklistProductType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      PicklistProductType thisPicklistProductType = _db.PicklistProductTypes.FirstOrDefault(picklistProductType => picklistProductType.PicklistProductTypeId == id);
      return View(thisPicklistProductType);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      PicklistProductType thisPicklistProductType = _db.PicklistProductTypes.FirstOrDefault(picklistProductType => picklistProductType.PicklistProductTypeId == id);
      _db.PicklistProductTypes.Remove(thisPicklistProductType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 
  }
}   