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

namespace VENUERP.Controllers
{
    [Authentication]
    public class PermissionsRolesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PermissionsRoles
        public async Task<ActionResult> Index()
        {
            var permissionsRole = db.PermissionsRole.Include(p => p.MapPages).Include(p => p.UserRoles);
            return View(await permissionsRole.ToListAsync());
        }

        // GET: PermissionsRoles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionsRole permissionsRole = await db.PermissionsRole.FindAsync(id);
            if (permissionsRole == null)
            {
                return HttpNotFound();
            }
            return View(permissionsRole);
        }

        // GET: PermissionsRoles/Create
        public ActionResult Create()
        {
            ViewBag.PageId = new SelectList(db.MapPages, "Id", "Pages");
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role");
            return View();
        }

        // POST: PermissionsRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PageId,RoleId,Add,Edit,Delete,View,CreatedOn,ModifiedOn")] PermissionsRole permissionsRole)
        {
            if (ModelState.IsValid)
            {
                db.PermissionsRole.Add(permissionsRole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PageId = new SelectList(db.MapPages, "Id", "Pages", permissionsRole.PageId);
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role", permissionsRole.RoleId);
            return View(permissionsRole);
        }

        // GET: PermissionsRoles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionsRole permissionsRole = await db.PermissionsRole.FindAsync(id);
            if (permissionsRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageId = new SelectList(db.MapPages, "Id", "Pages", permissionsRole.PageId);
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role", permissionsRole.RoleId);
            return View(permissionsRole);
        }

        // POST: PermissionsRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PageId,RoleId,Add,Edit,Delete,View,CreatedOn,ModifiedOn")] PermissionsRole permissionsRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissionsRole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PageId = new SelectList(db.MapPages, "Id", "Pages", permissionsRole.PageId);
            ViewBag.RoleId = new SelectList(db.UserRoles, "Id", "Role", permissionsRole.RoleId);
            return View(permissionsRole);
        }

        // GET: PermissionsRoles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionsRole permissionsRole = await db.PermissionsRole.FindAsync(id);
            if (permissionsRole == null)
            {
                return HttpNotFound();
            }
            return View(permissionsRole);
        }

        // POST: PermissionsRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PermissionsRole permissionsRole = await db.PermissionsRole.FindAsync(id);
            db.PermissionsRole.Remove(permissionsRole);
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
