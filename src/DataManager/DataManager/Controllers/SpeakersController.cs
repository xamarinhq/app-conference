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
    public class SpeakersController : Controller
    {
        private Techdays2016Repository db = new Techdays2016Repository();

        // GET: Speakers
        public async Task<ActionResult> Index()
        {
            return View(await db.Speakers.Where(s => s.Deleted == false).OrderBy(s=>s.FirstName).ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speaker speaker = await db.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return HttpNotFound();
            }
            return View(speaker);
        }

        // GET: Speakers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,IsFeatured,Biography,PhotoUrl,AvatarUrl,PositionName,CompanyName,CompanyWebsiteUrl,BlogUrl,TwitterUrl,LinkedInUrl,FacebookProfileName,Email")] Speaker speaker)
        {
            speaker.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                db.Speakers.Add(speaker);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speaker speaker = await db.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return HttpNotFound();
            }
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,IsFeatured,Biography,PhotoUrl,AvatarUrl,PositionName,CompanyName,CompanyWebsiteUrl,BlogUrl,TwitterUrl,FacebookProfileName,LinkedInUrl,Email")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speaker).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speaker speaker = await db.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return HttpNotFound();
            }
            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Speaker speaker = await db.Speakers.FindAsync(id);
            speaker.Deleted = true;
            db.Entry(speaker).State = EntityState.Modified;
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
