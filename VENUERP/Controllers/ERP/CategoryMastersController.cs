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
using VENUERP.Repository.Interface;
using VENUERP.Repository.Repository;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.ViewModels.ERP;

namespace VENUERP.Controllers.ERP
{
    public class CategoryMastersController : Controller
    {

        private DatabaseContext db = new DatabaseContext();
        private readonly ICategoryMasters _categoryMasters;
        public CategoryMastersController()
        {
            this._categoryMasters = new CategoryMastersRepository();
        }

        // GET: CategoryMasters
        public ActionResult Index()
        {             
            return View();
        }

        // GET: CategoryMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMaster categoryMaster = await db.CategoryMasters.FindAsync(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }

        // GET: CategoryMasters/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            return View();
        }

        // POST: CategoryMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,CategoryName,BrandId,ComCode")] CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                db.CategoryMasters.Add(categoryMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName", categoryMaster.BrandId);
            return View(categoryMaster);
        }

        // GET: CategoryMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMaster categoryMaster = await db.CategoryMasters.FindAsync(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName", categoryMaster.BrandId);
            return View(categoryMaster);
        }

        // POST: CategoryMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,CategoryName,BrandId,ComCode")] CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.BrandMasters, "BrandId", "BrandName", categoryMaster.BrandId);
            return View(categoryMaster);
        }

        // GET: CategoryMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMaster categoryMaster = await db.CategoryMasters.FindAsync(id);
            if (categoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(categoryMaster);
        }

        // POST: CategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategoryMaster categoryMaster = await db.CategoryMasters.FindAsync(id);
            db.CategoryMasters.Remove(categoryMaster);
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
            var categoryMasterViewModels  = _categoryMasters.GetCategoryMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                categoryMasterViewModels = categoryMasterViewModels.Where(x => x.Brand.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Category.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                categoryMasterViewModels = sortDirection == "asc" ? categoryMasterViewModels.OrderBy(c => c.Brand) : categoryMasterViewModels.OrderByDescending(c => c.Brand);
            }
            else if (sortColumnIndex == 4)
            {
                categoryMasterViewModels = sortDirection == "asc" ? categoryMasterViewModels.OrderBy(c => c.Category) : categoryMasterViewModels.OrderByDescending(c => c.Category);
            } 
            else
            {
                Func<CategoryMasterViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.Brand : sortColumnIndex == 1 ? e.Category  : e.Category;
                categoryMasterViewModels = sortDirection == "asc" ? categoryMasterViewModels.OrderBy(orderingFunction) : categoryMasterViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = categoryMasterViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = categoryMasterViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }

    }
}
