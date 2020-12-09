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
    public class SupplyReturnMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SupplyReturnMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.SupplyReturnMaster.ToListAsync());
        }

        // GET: SupplyReturnMasters/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyReturnMaster supplyReturnMaster = await db.SupplyReturnMaster.FindAsync(id);
            if (supplyReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplyReturnMaster);
        }

        // GET: SupplyReturnMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplyReturnMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,SupplyReturnDate,CustomerCode,SupplyReturnNo,ItemCode,Qty,CreatedDate,ConCode,UserId")] SupplyReturnMaster supplyReturnMaster)
        {
            if (ModelState.IsValid)
            {
                supplyReturnMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.SupplyReturnMaster.Add(supplyReturnMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(supplyReturnMaster);
        }

        // GET: SupplyReturnMasters/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyReturnMaster supplyReturnMaster = await db.SupplyReturnMaster.FindAsync(id);
            if (supplyReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplyReturnMaster);
        }

        // POST: SupplyReturnMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,SupplyReturnDate,CustomerCode,SupplyReturnNo,ItemCode,Qty,CreatedDate,ConCode,UserId")] SupplyReturnMaster supplyReturnMaster)
        {
            if (ModelState.IsValid)
            {
                supplyReturnMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(supplyReturnMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplyReturnMaster);
        }

        // GET: SupplyReturnMasters/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyReturnMaster supplyReturnMaster = await db.SupplyReturnMaster.FindAsync(id);
            if (supplyReturnMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplyReturnMaster);
        }

        // POST: SupplyReturnMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SupplyReturnMaster supplyReturnMaster = await db.SupplyReturnMaster.FindAsync(id);
            db.SupplyReturnMaster.Remove(supplyReturnMaster);
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
