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
    public class BankMastersController : Controller
    {
       
        private DatabaseContext db = new DatabaseContext();

        // GET: BankMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.BankMasters.ToListAsync());
        }

        // GET: BankMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankMaster = await db.BankMasters.FindAsync(id);
            if (bankMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankMaster);
        }

        // GET: BankMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BankSno,BANK,ACCOUNTNO,IFSCCode,HOLDERNAME,PaymentTerm1,PaymentTerm2,PaymentTerm3,ComCode,CreatedDate,UserId")] BankMaster bankMaster)
        {
            if (ModelState.IsValid)
            {
                bankMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.BankMasters.Add(bankMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bankMaster);
        }

        // GET: BankMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankMaster = await db.BankMasters.FindAsync(id);
            if (bankMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankMaster);
        }

        // POST: BankMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BankSno,BANK,ACCOUNTNO,IFSCCode,HOLDERNAME,PaymentTerm1,PaymentTerm2,PaymentTerm3,ComCode,CreatedDate,UserId")] BankMaster bankMaster)
        {
            if (ModelState.IsValid)
            {
                bankMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                db.Entry(bankMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bankMaster);
        }

        // GET: BankMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankMaster = await db.BankMasters.FindAsync(id);
            if (bankMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankMaster);
        }

        // POST: BankMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BankMaster bankMaster = await db.BankMasters.FindAsync(id);
            db.BankMasters.Remove(bankMaster);
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
