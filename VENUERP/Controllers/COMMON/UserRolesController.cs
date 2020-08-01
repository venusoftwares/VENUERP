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
    public class UserRolesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: UserRoles
        public async Task<ActionResult> Index()
        {
            return View(await db.UserRoles.ToListAsync());
        }

        // GET: UserRoles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoles userRoles = await db.UserRoles.FindAsync(id);
            if (userRoles == null)
            {
                return HttpNotFound();
            }
            return View(userRoles);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Role,Status,Dashboard")] UserRoles userRoles)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userRoles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userRoles);
        }

        // GET: UserRoles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoles userRoles = await db.UserRoles.FindAsync(id);
            if (userRoles == null)
            {
                return HttpNotFound();
            }
            return View(userRoles);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Role,Status,Dashboard")] UserRoles userRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRoles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userRoles);
        }

        // GET: UserRoles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoles userRoles = await db.UserRoles.FindAsync(id);
            if (userRoles == null)
            {
                return HttpNotFound();
            }
            return View(userRoles);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserRoles userRoles = await db.UserRoles.FindAsync(id);
            db.UserRoles.Remove(userRoles);
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
