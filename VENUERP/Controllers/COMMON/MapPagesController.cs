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
    public class MapPagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: MapPages
        public async Task<ActionResult> Index()
        {
            return View(await db.MapPages.ToListAsync());
        }

        // GET: MapPages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapPages mapPages = await db.MapPages.FindAsync(id);
            if (mapPages == null)
            {
                return HttpNotFound();
            }
            return View(mapPages);
        }

        // GET: MapPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Pages,Status,CreatedDate")] MapPages mapPages)
        {
            if (ModelState.IsValid)
            {
                db.MapPages.Add(mapPages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mapPages);
        }

        // GET: MapPages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapPages mapPages = await db.MapPages.FindAsync(id);
            if (mapPages == null)
            {
                return HttpNotFound();
            }
            return View(mapPages);
        }

        // POST: MapPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Pages,Status,CreatedDate")] MapPages mapPages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mapPages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mapPages);
        }

        // GET: MapPages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapPages mapPages = await db.MapPages.FindAsync(id);
            if (mapPages == null)
            {
                return HttpNotFound();
            }
            return View(mapPages);
        }

        // POST: MapPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MapPages mapPages = await db.MapPages.FindAsync(id);
            db.MapPages.Remove(mapPages);
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
