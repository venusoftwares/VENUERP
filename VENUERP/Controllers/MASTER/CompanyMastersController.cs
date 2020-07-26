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

namespace VENUERP.Controllers.MASTER
{
    [Authentication]
    public class CompanyMastersController : Controller
    {
        
        private DatabaseContext db = new DatabaseContext();

        // GET: CompanyMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyMasters.ToListAsync());
        }

        // GET: CompanyMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMaster companyMaster = await db.CompanyMasters.FindAsync(id);
            if (companyMaster == null)
            {
                return HttpNotFound();
            }
            return View(companyMaster);
        }

        // GET: CompanyMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ComCode,CompanyName,Address,Address2,PhoneNo,MobileNo,GSTNo,PinCode,City,WebSite,Email,Password")] CompanyMaster companyMaster)
        {
            if (ModelState.IsValid)
            {
                companyMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.CompanyMasters.Add(companyMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyMaster);
        }

        // GET: CompanyMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMaster companyMaster = await db.CompanyMasters.FindAsync(id);
            if (companyMaster == null)
            {
                return HttpNotFound();
            }
            return View(companyMaster);
        }

        // POST: CompanyMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ComCode,CompanyName,Address,Address2,PhoneNo,MobileNo,GSTNo,PinCode,City,WebSite,Email,Password")] CompanyMaster companyMaster)
        {
            if (ModelState.IsValid)
            {
                companyMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(companyMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyMaster);
        }

        // GET: CompanyMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMaster companyMaster = await db.CompanyMasters.FindAsync(id);
            if (companyMaster == null)
            {
                return HttpNotFound();
            }
            return View(companyMaster);
        }

        // POST: CompanyMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyMaster companyMaster = await db.CompanyMasters.FindAsync(id);
            db.CompanyMasters.Remove(companyMaster);
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
