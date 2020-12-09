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
    public class MeterialRateMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MeterialRateMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.MeterialRateMaster.ToListAsync());
        }

        // GET: MeterialRateMasters/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterialRateMaster meterialRateMaster = await db.MeterialRateMaster.FindAsync(id);
            if (meterialRateMaster == null)
            {
                return HttpNotFound();
            }
            return View(meterialRateMaster);
        }

        // GET: MeterialRateMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeterialRateMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,EffectDate,ItemCode,ItemRate,CreatedDate,ConCode,UserId")] MeterialRateMaster meterialRateMaster)
        {
            if (ModelState.IsValid)
            {
                meterialRateMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.MeterialRateMaster.Add(meterialRateMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(meterialRateMaster);
        }

        // GET: MeterialRateMasters/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterialRateMaster meterialRateMaster = await db.MeterialRateMaster.FindAsync(id);
            if (meterialRateMaster == null)
            {
                return HttpNotFound();
            }
            return View(meterialRateMaster);
        }

        // POST: MeterialRateMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,EffectDate,ItemCode,ItemRate,CreatedDate,ConCode,UserId")] MeterialRateMaster meterialRateMaster)
        {
            if (ModelState.IsValid)
            {
                meterialRateMaster.ConCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(meterialRateMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meterialRateMaster);
        }

        // GET: MeterialRateMasters/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeterialRateMaster meterialRateMaster = await db.MeterialRateMaster.FindAsync(id);
            if (meterialRateMaster == null)
            {
                return HttpNotFound();
            }
            return View(meterialRateMaster);
        }

        // POST: MeterialRateMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            MeterialRateMaster meterialRateMaster = await db.MeterialRateMaster.FindAsync(id);
            db.MeterialRateMaster.Remove(meterialRateMaster);
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
