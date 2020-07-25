

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VENUERP.Models;
using VENUERP.Repository.Interface;
using VENUERP.Repository.Repository;
using VENUERP.ViewModels.ERP;
using VENUERP.ViewModels.JQUERYDATATABLES;

namespace VENUERP.Controllers.ERP
{
    public class BrandMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly IBrandMaster _brandMaster;
        public BrandMastersController()
        {
            this._brandMaster = new BrandMasterRepository();
        }
        // GET: BrandMasters
        public ActionResult Index()
        {
            return View();
        }
       
        // GET: BrandMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandMaster brandMaster = await db.BrandMasters.FindAsync(id);
            if (brandMaster == null)
            {
                return HttpNotFound();
            }
            return View(brandMaster);
        }

        // GET: BrandMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BrandId,BrandName,ComCode")] BrandMaster brandMaster)
        {
            if (ModelState.IsValid)
            {
                brandMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.BrandMasters.Add(brandMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(brandMaster);
        }

        // GET: BrandMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandMaster brandMaster = await db.BrandMasters.FindAsync(id);
            if (brandMaster == null)
            {
                return HttpNotFound();
            }
            return View(brandMaster);
        }

        // POST: BrandMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BrandId,BrandName,ComCode")] BrandMaster brandMaster)
        {
            if (ModelState.IsValid)
            {
                brandMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(brandMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brandMaster);
        }

        // GET: BrandMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandMaster brandMaster = await db.BrandMasters.FindAsync(id);
            if (brandMaster == null)
            {
                return HttpNotFound();
            }
            return View(brandMaster);
        }

        // POST: BrandMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BrandMaster brandMaster = await db.BrandMasters.FindAsync(id);
            db.BrandMasters.Remove(brandMaster);
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
            var employees = _brandMaster.GetBrandMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                employees = employees.Where(x => x.Brand.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                employees = sortDirection == "asc" ? employees.OrderBy(c => c.Brand) : employees.OrderByDescending(c => c.Brand);
            }
            else
            {
                Func<BrandMasterViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.Brand : e.Brand;
                employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
            }
            var displayResult = employees.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = employees.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}
