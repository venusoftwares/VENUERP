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
using VENUERP.Repository.Interface;
using VENUERP.Repository.Repository;
using VENUERP.ViewModels.ERP;
using VENUERP.Providers;

namespace VENUERP.Controllers.ERP
{
    [Authentication]
    public class CustomerMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly ICustomerMasters _customerMasters;

        public CustomerMastersController()
        {
            this._customerMasters = new CustomerMastersRepository();
        }

        // GET: CustomerMasters
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = await db.CustomerMasters.FindAsync(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerId,Name,Address,ContactPerson,ContactNo,GSTNo,Email,ComCode")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                customerMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.CustomerMasters.Add(customerMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customerMaster);
        }

        // GET: CustomerMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = await db.CustomerMasters.FindAsync(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId,Name,Address,ContactPerson,ContactNo,GSTNo,Email,ComCode")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                customerMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(customerMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = await db.CustomerMasters.FindAsync(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomerMaster customerMaster = await db.CustomerMasters.FindAsync(id);
            db.CustomerMasters.Remove(customerMaster);
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
            var customerMasterViewModels  = _customerMasters.GetCustomerrMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customerMasterViewModels = customerMasterViewModels.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Address.ToLower().Contains(param.sSearch.ToLower())
                                              || x.MobileNo.ToLower().Contains(param.sSearch.ToLower())
                                              || x.GSTNo.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                customerMasterViewModels = sortDirection == "asc" ? customerMasterViewModels.OrderBy(c => c.Name) : customerMasterViewModels.OrderByDescending(c => c.Name);
            }
            else if (sortColumnIndex == 4)
            {
                customerMasterViewModels = sortDirection == "asc" ? customerMasterViewModels.OrderBy(c => c.Address) : customerMasterViewModels.OrderByDescending(c => c.Address);
            }
            else if (sortColumnIndex == 5)
            {
                customerMasterViewModels = sortDirection == "asc" ? customerMasterViewModels.OrderBy(c => c.MobileNo) : customerMasterViewModels.OrderByDescending(c => c.MobileNo);
            }
            else if (sortColumnIndex == 6)
            {
                customerMasterViewModels = sortDirection == "asc" ? customerMasterViewModels.OrderBy(c => c.GSTNo) : customerMasterViewModels.OrderByDescending(c => c.GSTNo);
            }
            else
            {
                Func<CustomerMasterViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name : e.Name;
                customerMasterViewModels = sortDirection == "asc" ? customerMasterViewModels.OrderBy(orderingFunction) : customerMasterViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = customerMasterViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = customerMasterViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}
