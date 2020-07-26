﻿using System;
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

namespace VENUERP.Controllers
{
    [Authentication]
    public class CashReceivedController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly ISQLStored _sQLStored;
        public CashReceivedController()
        {
            this._sQLStored = new SQLStoredProcedure();
        }

        // GET: CashReceived
        public async Task<ActionResult> Index()
        {
            var cashMasters = db.CashMasters.Include(c => c.CustomerMaster).Include(c => c.SupplierMaster);
            return View(await cashMasters.Where(x=>x.Nature== "Receipt").ToListAsync());
        }

        // GET: CashReceived/Details/5
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

        // GET: CashReceived/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName");
            return View();
        }

        // POST: CashReceived/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CashId,VoucherNo,Nature,Date,CustomerId,SupplierID,Amount,Description")] CashMaster cashMaster)
        {
            if (ModelState.IsValid)
            {
                cashMaster.Nature = "Receipt";
                db.CashMasters.Add(cashMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", cashMaster.CustomerId);
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", cashMaster.SupplierID);
            return View(cashMaster);
        }

        // GET: CashReceived/Edit/5
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

        // POST: CashReceived/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CashId,VoucherNo,Nature,Date,CustomerId,SupplierID,Amount,Description")] CashMaster cashMaster)
        {
            if (ModelState.IsValid)
            {
                cashMaster.Nature = "Receipt";
                db.Entry(cashMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", cashMaster.CustomerId);
            ViewBag.SupplierID = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", cashMaster.SupplierID);
            return View(cashMaster);
        }

        // GET: CashReceived/Delete/5
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

        // POST: CashReceived/Delete/5
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
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_IQuotation_CustomerDetails");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/IQuotationCustomer.rpt", ds);
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
    }
}
