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
    public class PurchaseReturnMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PurchaseReturnMasters
        public async Task<ActionResult> Index()
        {
            var purchaseReturnMasters = db.PurchaseReturnMasters.Include(p => p.SupplierMaster);
            return View(await purchaseReturnMasters.ToListAsync());
        }

        // GET: PurchaseReturnMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseReturnMaster purchaseReturnMaster = await db.PurchaseReturnMasters.FindAsync(id);
            if (purchaseReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(purchaseReturnMaster);
        }

        // GET: PurchaseReturnMasters/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName");
            return View();
        }

        // POST: PurchaseReturnMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PurchaseReturnID,PurchaseReturnDate,InvoiceNo,SupplierId,IsCash,CGSTAmt,CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] PurchaseReturnMaster purchaseReturnMaster)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseReturnMasters.Add(purchaseReturnMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", purchaseReturnMaster.SupplierId);
            return View(purchaseReturnMaster);
        }

        // GET: PurchaseReturnMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseReturnMaster purchaseReturnMaster = await db.PurchaseReturnMasters.FindAsync(id);
            if (purchaseReturnMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", purchaseReturnMaster.SupplierId);
            return View(purchaseReturnMaster);
        }

        // POST: PurchaseReturnMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PurchaseReturnID,PurchaseReturnDate,InvoiceNo,SupplierId,IsCash,CGSTAmt,CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] PurchaseReturnMaster purchaseReturnMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseReturnMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", purchaseReturnMaster.SupplierId);
            return View(purchaseReturnMaster);
        }

        // GET: PurchaseReturnMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseReturnMaster purchaseReturnMaster = await db.PurchaseReturnMasters.FindAsync(id);
            if (purchaseReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(purchaseReturnMaster);
        }

        // POST: PurchaseReturnMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PurchaseReturnMaster purchaseReturnMaster = await db.PurchaseReturnMasters.FindAsync(id);
            db.PurchaseReturnMasters.Remove(purchaseReturnMaster);
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
