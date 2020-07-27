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
using VENUERP.Repository.Interface.TRANSACTION;
using VENUERP.Repositoy.TRANSACTION;
using VENUERP.Providers;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.Repository.Interface.PAYMENT;
using VENUERP.Repository.Repository.PAYMENT;
using VENUERP.ViewModels.PAYMENT;

namespace VENUERP.Controllers
{
    [Authentication]
    public class CashPaymentController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly ISQLStored _sQLStored;
        private readonly ICashPayment _cashPayment;

        public CashPaymentController()
        {
            this._sQLStored = new SQLStoredProcedure();
            this._cashPayment = new CashPaymentRepository();
        }
        // GET: CashPayment
        public async Task<ActionResult> Index()
        {
            var cashMasters = db.CashMasters.Include(c => c.CustomerMaster).Include(c => c.SupplierMaster);
            return View(await cashMasters.Where(x=>x.Nature== "Payment").ToListAsync());
        }

        // GET: CashPayment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashMaster cashMaster = await db.CashMasters.FindAsync(id);
            if (cashMaster == null)
            {
                return HttpNotFound();
            }
            return View(cashMaster);
        }

        // GET: CashPayment/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName"); 
            return View();
        }

        // POST: CashPayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CashId,VoucherNo,Nature,Date,CustomerId,SupplierID,Amount,Description,ComCode")] CashMaster cashMaster)
        {
            if (ModelState.IsValid)
            {
                cashMaster.Nature = "Payment";
                cashMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.CashMasters.Add(cashMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", cashMaster.CustomerId);
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", cashMaster.SupplierID);
            return View(cashMaster);
        }

        // GET: CashPayment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashMaster cashMaster = await db.CashMasters.FindAsync(id);
            if (cashMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", cashMaster.CustomerId);
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", cashMaster.SupplierID);
            return View(cashMaster);
        }

        // POST: CashPayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CashId,VoucherNo,Nature,Date,CustomerId,SupplierID,Amount,Description,ComCode")] CashMaster cashMaster)
        {
            if (ModelState.IsValid)
            {
                cashMaster.Nature = "Payment";
                cashMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(cashMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", cashMaster.CustomerId);
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", cashMaster.SupplierID);
            return View(cashMaster);
        }

        // GET: CashPayment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashMaster cashMaster = await db.CashMasters.FindAsync(id);
            if (cashMaster == null)
            {
                return HttpNotFound();
            }
            return View(cashMaster);
        }

        // POST: CashPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CashMaster cashMaster = await db.CashMasters.FindAsync(id);
            db.CashMasters.Remove(cashMaster);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult Print(int ID)
        {            
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_CashPayment_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/CashPayment.rpt", ds);
            }

            return View();

        }
        public void ReportFiled(string Filename, DataSet ds)
        {
            try
            {
                Session["Path"] = Filename;
                Session["Source"] = ds.Tables[0];
                Response.Redirect("~/Reports/Reports.aspx");
            }
            catch (Exception e)
            {
                throw e;
            }
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
            var cashPaymentViewModels  = _cashPayment.GetCashPaymentDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                cashPaymentViewModels = cashPaymentViewModels.Where(x => x.Date.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.No.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Amount.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                cashPaymentViewModels = sortDirection == "asc" ? cashPaymentViewModels.OrderBy(c => c.No) : cashPaymentViewModels.OrderByDescending(c => c.No);
            }
            else if (sortColumnIndex == 4)
            {
                cashPaymentViewModels = sortDirection == "asc" ? cashPaymentViewModels.OrderBy(c => c.Date) : cashPaymentViewModels.OrderByDescending(c => c.Date);
            }
            else if (sortColumnIndex == 5)
            {
                cashPaymentViewModels = sortDirection == "asc" ? cashPaymentViewModels.OrderBy(c => c.Name) : cashPaymentViewModels.OrderByDescending(c => c.Name);
            }
            else if (sortColumnIndex == 6)
            {
                cashPaymentViewModels = sortDirection == "asc" ? cashPaymentViewModels.OrderBy(c => c.Amount) : cashPaymentViewModels.OrderByDescending(c => c.Amount);
            }
            else
            {
                Func<CashPaymentViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.No : sortColumnIndex == 1 ? e.Date : e.Name;
                cashPaymentViewModels = sortDirection == "asc" ? cashPaymentViewModels.OrderBy(orderingFunction) : cashPaymentViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = cashPaymentViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = cashPaymentViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}
