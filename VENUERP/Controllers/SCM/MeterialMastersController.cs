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
    public class MeterialMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MeterialMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.MeterialMaster.ToListAsync());
        }

        // GET: MeterialMasters/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterialMaster meterialMaster = await db.MeterialMaster.FindAsync(id);
            if (meterialMaster == null)
            {
                return HttpNotFound();
            }
            return View(meterialMaster);
        }

        // GET: MeterialMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeterialMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,BrandCode,CatCode,ItemName,CreatedDate,ConCode,UserId")] MeterialMaster meterialMaster)
        {
            if (ModelState.IsValid)
            {
                meterialMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.MeterialMaster.Add(meterialMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(meterialMaster);
        }

        // GET: MeterialMasters/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterialMaster meterialMaster = await db.MeterialMaster.FindAsync(id);
            if (meterialMaster == null)
            {
                return HttpNotFound();
            }
            return View(meterialMaster);
        }

        // POST: MeterialMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,BrandCode,CatCode,ItemName,CreatedDate,ConCode,UserId")] MeterialMaster meterialMaster)
        {
            if (ModelState.IsValid)
            {
                meterialMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(meterialMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meterialMaster);
        }

        // GET: MeterialMasters/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterialMaster meterialMaster = await db.MeterialMaster.FindAsync(id);
            if (meterialMaster == null)
            {
                return HttpNotFound();
            }
            return View(meterialMaster);
        }

        // POST: MeterialMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            MeterialMaster meterialMaster = await db.MeterialMaster.FindAsync(id);
            db.MeterialMaster.Remove(meterialMaster);
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
