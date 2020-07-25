using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VENUERP.Models;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.ViewModels.ERP;
using VENUERP.Repository.Interface;
using VENUERP.Repository.Repository;

namespace VENUERP.Controllers.ERP
{
    public class ItemMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly IItemMasters _itemMasters;
        public ItemMastersController()
        {
            this._itemMasters = new ItemMastersRepository();
        }

        // GET: ItemMasters
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: ItemMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = await db.ItemMasters.FindAsync(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // GET: ItemMasters/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ItemMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemId,Dimension,BrandId,CategoryId,Description,ComCode,Rate,SCGST,SCGSTRate,SSGST,SSGSTRate,SIGST,SIGSTRate")] ItemMaster itemMaster)
        {
            if (ModelState.IsValid)
            {
                db.ItemMasters.Add(itemMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName", itemMaster.BrandId);
            ViewBag.CategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", itemMaster.CategoryId);
            return View(itemMaster);
        }

        // GET: ItemMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = await db.ItemMasters.FindAsync(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName", itemMaster.BrandId);
            ViewBag.CategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", itemMaster.CategoryId);
            return View(itemMaster);
        }

        // POST: ItemMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,Dimension,BrandId,CategoryId,Description,ComCode,Rate,SCGST,SCGSTRate,SSGST,SSGSTRate,SIGST,SIGSTRate")] ItemMaster itemMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName", itemMaster.BrandId);
            ViewBag.CategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", itemMaster.CategoryId);
            return View(itemMaster);
        }

        // GET: ItemMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = await db.ItemMasters.FindAsync(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // POST: ItemMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ItemMaster itemMaster = await db.ItemMasters.FindAsync(id);
            db.ItemMasters.Remove(itemMaster);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var itemMasterViewModels = _itemMasters.GetItemMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                itemMasterViewModels = itemMasterViewModels.Where(x => x.Brand.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Category.ToLower().Contains(param.sSearch.ToLower())
                                              || x.HsnCode.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Item.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                itemMasterViewModels = sortDirection == "asc" ? itemMasterViewModels.OrderBy(c => c.HsnCode) : itemMasterViewModels.OrderByDescending(c => c.HsnCode);
            }
            else if (sortColumnIndex == 4)
            {
                itemMasterViewModels = sortDirection == "asc" ? itemMasterViewModels.OrderBy(c => c.Brand) : itemMasterViewModels.OrderByDescending(c => c.Brand);
            }
            else if (sortColumnIndex == 5)
            {
                itemMasterViewModels = sortDirection == "asc" ? itemMasterViewModels.OrderBy(c => c.Category) : itemMasterViewModels.OrderByDescending(c => c.Category);
            }
            else if (sortColumnIndex == 6)
            {
                itemMasterViewModels = sortDirection == "asc" ? itemMasterViewModels.OrderBy(c => c.Item) : itemMasterViewModels.OrderByDescending(c => c.Item);
            }
            else
            {
                Func<ItemMasterViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.Brand : sortColumnIndex == 1 ? e.Category : e.Category;
                itemMasterViewModels = sortDirection == "asc" ? itemMasterViewModels.OrderBy(orderingFunction) : itemMasterViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = itemMasterViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = itemMasterViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}
