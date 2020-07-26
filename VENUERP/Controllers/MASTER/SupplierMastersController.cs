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
using VENUERP.Providers;

namespace VENUERP.Controllers.ERP
{
    [Authentication]
    public class SupplierMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly ISupplierMasters _supplierMasters;

        public SupplierMastersController()
        {
            this._supplierMasters = new SupplierMastersRepository();
        }
        // GET: SupplierMasters
        public ActionResult Index()
        {
            return View();
        }

        // GET: SupplierMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = await db.SupplierMasters.FindAsync(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }

        // GET: SupplierMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SupplierId,SupplierName,SupplierAddress,ContactPerson,ContactNo,GSTNo,ComCode")] SupplierMaster supplierMaster)
        {
            if (ModelState.IsValid)
            {
                supplierMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.SupplierMasters.Add(supplierMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(supplierMaster);
        }

        // GET: SupplierMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = await db.SupplierMasters.FindAsync(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }

        // POST: SupplierMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SupplierId,SupplierName,SupplierAddress,ContactPerson,ContactNo,GSTNo,ComCode")] SupplierMaster supplierMaster)
        {
            if (ModelState.IsValid)
            {
                supplierMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(supplierMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplierMaster);
        }

        // GET: SupplierMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierMaster supplierMaster = await db.SupplierMasters.FindAsync(id);
            if (supplierMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplierMaster);
        }

        // POST: SupplierMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SupplierMaster supplierMaster = await db.SupplierMasters.FindAsync(id);
            db.SupplierMasters.Remove(supplierMaster);
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
            var supplierMasterViewModels  = _supplierMasters.GetSupplierMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                supplierMasterViewModels = supplierMasterViewModels.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Address.ToLower().Contains(param.sSearch.ToLower())
                                              || x.MobileNo.ToLower().Contains(param.sSearch.ToLower())
                                              || x.GSTNo.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                supplierMasterViewModels = sortDirection == "asc" ? supplierMasterViewModels.OrderBy(c => c.Name) : supplierMasterViewModels.OrderByDescending(c => c.Name);
            }
            else if (sortColumnIndex == 4)
            {
                supplierMasterViewModels = sortDirection == "asc" ? supplierMasterViewModels.OrderBy(c => c.Address) : supplierMasterViewModels.OrderByDescending(c => c.Address);
            }
            else if (sortColumnIndex == 5)
            {
                supplierMasterViewModels = sortDirection == "asc" ? supplierMasterViewModels.OrderBy(c => c.MobileNo) : supplierMasterViewModels.OrderByDescending(c => c.MobileNo);
            }
            else if (sortColumnIndex == 6)
            {
                supplierMasterViewModels = sortDirection == "asc" ? supplierMasterViewModels.OrderBy(c => c.GSTNo) : supplierMasterViewModels.OrderByDescending(c => c.GSTNo);
            }
            else
            {
                Func<SupplierMasterViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name : e.Name;
                supplierMasterViewModels = sortDirection == "asc" ? supplierMasterViewModels.OrderBy(orderingFunction) : supplierMasterViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = supplierMasterViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = supplierMasterViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}
