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
    public class SupplyMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SupplyMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.SupplyMaster.ToListAsync());
        }

        // GET: SupplyMasters/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyMaster supplyMaster = await db.SupplyMaster.FindAsync(id);
            if (supplyMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplyMaster);
        }

        // GET: SupplyMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplyMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,SupplyDate,CustomerCode,SupplyNo,ItemCode,Qty,CreatedDate,ConCode,UserId")] SupplyMaster supplyMaster)
        {
            if (ModelState.IsValid)
            {
                supplyMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.SupplyMaster.Add(supplyMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(supplyMaster);
        }

        // GET: SupplyMasters/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyMaster supplyMaster = await db.SupplyMaster.FindAsync(id);
            if (supplyMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplyMaster);
        }

        // POST: SupplyMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,SupplyDate,CustomerCode,SupplyNo,ItemCode,Qty,CreatedDate,ConCode,UserId")] SupplyMaster supplyMaster)
        {
            if (ModelState.IsValid)
            {
                supplyMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(supplyMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplyMaster);
        }

        // GET: SupplyMasters/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyMaster supplyMaster = await db.SupplyMaster.FindAsync(id);
            if (supplyMaster == null)
            {
                return HttpNotFound();
            }
            return View(supplyMaster);
        }

        // POST: SupplyMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SupplyMaster supplyMaster = await db.SupplyMaster.FindAsync(id);
            db.SupplyMaster.Remove(supplyMaster);
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
