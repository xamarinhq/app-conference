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
    public class SponsorLevelsController : Controller
    {
        private Techdays2016Repository db = new Techdays2016Repository();

        // GET: SponsorLevels
        public async Task<ActionResult> Index()
        {
            return View(await db.SponsorLevels.Where(s => s.Deleted == false).ToListAsync());
        }

        // GET: SponsorLevels/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SponsorLevel sponsorLevel = await db.SponsorLevels.FindAsync(id);
            if (sponsorLevel == null)
            {
                return HttpNotFound();
            }
            return View(sponsorLevel);
        }

        // GET: SponsorLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SponsorLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Rank")] SponsorLevel sponsorLevel)
        {
            if (ModelState.IsValid)
            {
                sponsorLevel.Id = Guid.NewGuid().ToString();
                db.SponsorLevels.Add(sponsorLevel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sponsorLevel);
        }

        // GET: SponsorLevels/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SponsorLevel sponsorLevel = await db.SponsorLevels.FindAsync(id);
            if (sponsorLevel == null)
            {
                return HttpNotFound();
            }
            return View(sponsorLevel);
        }

        // POST: SponsorLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Rank")] SponsorLevel sponsorLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sponsorLevel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sponsorLevel);
        }

        // GET: SponsorLevels/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SponsorLevel sponsorLevel = await db.SponsorLevels.FindAsync(id);
            if (sponsorLevel == null)
            {
                return HttpNotFound();
            }
            return View(sponsorLevel);
        }

        // POST: SponsorLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            SponsorLevel sponsorLevel = await db.SponsorLevels.FindAsync(id);
            sponsorLevel.Deleted = true;
            db.Entry(sponsorLevel).State = EntityState.Modified;
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
