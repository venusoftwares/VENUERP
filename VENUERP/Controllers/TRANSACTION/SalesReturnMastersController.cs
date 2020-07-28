using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VERP.Models;

namespace VERP.Controllers
{
    public class SalesReturnMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SalesReturnMasters
        public async Task<ActionResult> Index()
        {
            var salesReturnMasters = db.SalesReturnMasters.Include(s => s.CustomerMaster);
            return View(await salesReturnMasters.ToListAsync());
        }

        // GET: SalesReturnMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesReturnMaster salesReturnMaster = await db.SalesReturnMasters.FindAsync(id);
            if (salesReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesReturnMaster);
        }

        // GET: SalesReturnMasters/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.CustomerMasters, "CustomerId", "Name");
            return View();
        }

        // POST: SalesReturnMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SalesReturnID,SalesDate,InvoiceNo,CustomerID,IsCash,CGSTAmt,CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] SalesReturnMaster salesReturnMaster)
        {
            if (ModelState.IsValid)
            {
                db.SalesReturnMasters.Add(salesReturnMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesReturnMaster.CustomerID);
            return View(salesReturnMaster);
        }

        // GET: SalesReturnMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesReturnMaster salesReturnMaster = await db.SalesReturnMasters.FindAsync(id);
            if (salesReturnMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesReturnMaster.CustomerID);
            return View(salesReturnMaster);
        }

        // POST: SalesReturnMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SalesReturnID,SalesDate,InvoiceNo,CustomerID,IsCash,CGSTAmt,CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] SalesReturnMaster salesReturnMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesReturnMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesReturnMaster.CustomerID);
            return View(salesReturnMaster);
        }

        // GET: SalesReturnMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesReturnMaster salesReturnMaster = await db.SalesReturnMasters.FindAsync(id);
            if (salesReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesReturnMaster);
        }

        // POST: SalesReturnMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SalesReturnMaster salesReturnMaster = await db.SalesReturnMasters.FindAsync(id);
            db.SalesReturnMasters.Remove(salesReturnMaster);
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
    }
}
