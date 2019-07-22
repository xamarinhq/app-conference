using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataManager.Models;
using DataManager.Repository;

namespace DataManager.Controllers
{
    [Authorize]
    public class MiniHacksController : Controller
    {
        private LaComarcaRepository db = new LaComarcaRepository();

        // GET: MiniHacks
        public async Task<ActionResult> Index()
        {
            return View(await db.MiniHacks.Where(s => s.Deleted == false).ToListAsync());
        }

        // GET: MiniHacks/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MiniHack miniHack = await db.MiniHacks.FindAsync(id);
            if (miniHack == null)
            {
                return HttpNotFound();
            }
            return View(miniHack);
        }

        // GET: MiniHacks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MiniHacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Subtitle,Description,GitHubUrl,BadgeUrl,UnlockCode,Score,Category")] MiniHack miniHack)
        {
            miniHack.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                miniHack.Id = Guid.NewGuid().ToString();
                db.MiniHacks.Add(miniHack);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(miniHack);
        }

        // GET: MiniHacks/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MiniHack miniHack = await db.MiniHacks.FindAsync(id);
            if (miniHack == null)
            {
                return HttpNotFound();
            }
            return View(miniHack);
        }

        // POST: MiniHacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Subtitle,Description,GitHubUrl,BadgeUrl,UnlockCode,Score,Category")] MiniHack miniHack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(miniHack).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(miniHack);
        }

        // GET: MiniHacks/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MiniHack miniHack = await db.MiniHacks.FindAsync(id);
            if (miniHack == null)
            {
                return HttpNotFound();
            }
            return View(miniHack);
        }

        // POST: MiniHacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            MiniHack miniHack = await db.MiniHacks.FindAsync(id);
            miniHack.Deleted = true;
            db.Entry(miniHack).State = EntityState.Modified;
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
