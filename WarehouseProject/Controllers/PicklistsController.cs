using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WarehouseProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseProject.Controllers
{
  public class PicklistsController : Controller
  {
    private readonly WarehouseProjectContext _db;

    public PicklistsController(WarehouseProjectContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Picklist> model = _db.Picklists.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Picklist picklist)
    {
      _db.Picklists.Add(picklist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Picklist thisPicklist = _db.Picklists
                                  .Include(pic => pic.Products)
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

  }
}   