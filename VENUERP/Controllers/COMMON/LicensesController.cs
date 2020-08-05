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
using VENUERP.Providers;

namespace VENUERP.Controllers.COMMON
{
    [LicenseAuthentication]
    public class LicensesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Licenses
        public async Task<ActionResult> Index()
        {
            var licenses = db.Licenses.Include(l => l.companyMaster);
            return View(await licenses.ToListAsync());
        }

        // GET: Licenses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = await db.Licenses.FindAsync(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        // GET: Licenses/Create
        public ActionResult Create()
        {
            ViewBag.ComCode = new SelectList(db.CompanyMasters, "ComCode", "CompanyName");
            return View();
        }

        // POST: Licenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lsno,ComCode,ToDate,FromDate")] License license)
        {
            if (ModelState.IsValid)
            {
                db.Licenses.Add(license);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ComCode = new SelectList(db.CompanyMasters, "ComCode", "CompanyName", license.ComCode);
            return View(license);
        }

        // GET: Licenses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = await db.Licenses.FindAsync(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComCode = new SelectList(db.CompanyMasters, "ComCode", "CompanyName", license.ComCode);
            return View(license);
        }

        // POST: Licenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lsno,ComCode,ToDate,FromDate")] License license)
        {
            if (ModelState.IsValid)
            {
                db.Entry(license).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ComCode = new SelectList(db.CompanyMasters, "ComCode", "CompanyName", license.ComCode);
            return View(license);
        }

        // GET: Licenses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            License license = await db.Licenses.FindAsync(id);
            if (license == null)
            {
                return HttpNotFound();
            }
            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            License license = await db.Licenses.FindAsync(id);
            db.Licenses.Remove(license);
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
