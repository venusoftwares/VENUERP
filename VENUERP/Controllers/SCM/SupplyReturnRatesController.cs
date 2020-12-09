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
using VENUERP.Models.SCM;

namespace VENUERP.Controllers.SCM
{
    public class SupplyReturnRatesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SupplyReturnRates
        public async Task<ActionResult> Index()
        {
            return View(await db.SupplyReturnRate.ToListAsync());
        }

        // GET: SupplyReturnRates/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyReturnRate supplyReturnRate = await db.SupplyReturnRate.FindAsync(id);
            if (supplyReturnRate == null)
            {
                return HttpNotFound();
            }
            return View(supplyReturnRate);
        }

        // GET: SupplyReturnRates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplyReturnRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,ItemCode,FromDate,ToDate,SupplyCode,SupplyReturnCode,PerDayRate,Qty,TotalDays,Discount,Amount,CreatedDate,UserId,CustomerCode")] SupplyReturnRate supplyReturnRate)
        {
            if (ModelState.IsValid)
            {
                db.SupplyReturnRate.Add(supplyReturnRate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(supplyReturnRate);
        }

        // GET: SupplyReturnRates/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyReturnRate supplyReturnRate = await db.SupplyReturnRate.FindAsync(id);
            if (supplyReturnRate == null)
            {
                return HttpNotFound();
            }
            return View(supplyReturnRate);
        }

        // POST: SupplyReturnRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,ItemCode,FromDate,ToDate,SupplyCode,SupplyReturnCode,PerDayRate,Qty,TotalDays,Discount,Amount,CreatedDate,UserId,CustomerCode")] SupplyReturnRate supplyReturnRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplyReturnRate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplyReturnRate);
        }

        // GET: SupplyReturnRates/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyReturnRate supplyReturnRate = await db.SupplyReturnRate.FindAsync(id);
            if (supplyReturnRate == null)
            {
                return HttpNotFound();
            }
            return View(supplyReturnRate);
        }

        // POST: SupplyReturnRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SupplyReturnRate supplyReturnRate = await db.SupplyReturnRate.FindAsync(id);
            db.SupplyReturnRate.Remove(supplyReturnRate);
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
