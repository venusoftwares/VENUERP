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
    [Authentication]
    public class LoginsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Logins
        public async Task<ActionResult> Index()
        {
            var logins = db.Logins.Include(l => l.UserRoles);
            return View(await logins.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LoginId,Username,Password,CreatedDate,ComCode,RoleId")] Login login)
        {
            if (ModelState.IsValid)
            {
                login.ComCode = Convert.ToInt32(Session["ComCode"]);
                login.CreatedDate = DateTime.Now;
                db.Logins.Add(login);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role", login.RoleId);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role", login.RoleId);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LoginId,Username,Password,CreatedDate,ComCode,RoleId")] Login login)
        {
            if (ModelState.IsValid)
            {
                login.ComCode = Convert.ToInt32(Session["ComCode"]);
                login.CreatedDate = DateTime.Now;
                db.Entry(login).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role", login.RoleId);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Login login = await db.Logins.FindAsync(id);
            db.Logins.Remove(login);
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
