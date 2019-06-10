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
using DataManager.ViewModels;

namespace DataManager.Controllers
{
    [Authorize]
    public class SponsorsController : Controller
    {
        private Techdays2016Repository db = new Techdays2016Repository();

        // GET: Sponsors
        public async Task<ActionResult> Index()
        {
            var sponsor = await db.Sponsors.Where(s => s.Deleted == false).Include("SponsorLevel").ToListAsync();
            var subselectie = sponsor.GroupBy(s=>s.SponsorLevel).OrderBy(s => s.Key.Rank).ToList();
            var sponsors = subselectie.SelectMany(grp => grp);
            return View(sponsors);
        }

        // GET: Sponsors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = await db.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            await db.Entry(sponsor).Reference("SponsorLevel").LoadAsync();
            return View(sponsor);
        }

        // GET: Sponsors/Create
        public async Task<ActionResult> Create()
        {
            var model = await CreateViewmodel();
            return View(model);
        }

        private async Task<SponsorViewModel> CreateViewmodel(Sponsor sponsor = null)
        {
            if (sponsor == null)
                sponsor = new Sponsor();
            var model = new SponsorViewModel()
            {
                TrackedSponsor = sponsor,
                AvailableSponsorLevels = await db.SponsorLevels.OrderBy(l => l.Rank).ToListAsync(),
                SubmittedSponsorLevel = "",
            };

            return model;
        }

        // POST: Sponsors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SponsorViewModel sponsor)
        {
            if (ModelState.IsValid)
            {
                sponsor.TrackedSponsor.Id = Guid.NewGuid().ToString();
                sponsor.TrackedSponsor.SponsorLevel = await GetSponsorLevelForId(sponsor.SubmittedSponsorLevel);
                db.Sponsors.Add(sponsor.TrackedSponsor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sponsor);
        }

        private async Task<SponsorLevel> GetSponsorLevelForId(string submittedSponsorLevel)
        {
            var item = await db.SponsorLevels.FindAsync(submittedSponsorLevel);
            return item;
        }


        // GET: Sponsors/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = await db.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }

            var model = await CreateViewmodel(sponsor);

            return View(model);
        }

        // POST: Sponsors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SponsorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sponsorToUpdate = db.Sponsors.Where(s => s.Id == viewModel.TrackedSponsor.Id).Single();
                sponsorToUpdate.SponsorLevel = await GetSponsorLevelForId(viewModel.SubmittedSponsorLevel);
                sponsorToUpdate.BoothLocation = viewModel.TrackedSponsor.BoothLocation;
                sponsorToUpdate.Description = viewModel.TrackedSponsor.Description;
                sponsorToUpdate.ImageUrl = viewModel.TrackedSponsor.ImageUrl;
                sponsorToUpdate.Name = viewModel.TrackedSponsor.Name;
                sponsorToUpdate.Rank = viewModel.TrackedSponsor.Rank;
                sponsorToUpdate.TwitterUrl = viewModel.TrackedSponsor.TwitterUrl;
                sponsorToUpdate.WebsiteUrl = viewModel.TrackedSponsor.WebsiteUrl;
                sponsorToUpdate.FacebookProfileName = viewModel.TrackedSponsor.FacebookProfileName;
                sponsorToUpdate.LinkedInUrl = viewModel.TrackedSponsor.LinkedInUrl;

                db.Entry(sponsorToUpdate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Sponsors/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = await db.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            await db.Entry(sponsor).Reference("SponsorLevel").LoadAsync();
            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Sponsor sponsor = await db.Sponsors.FindAsync(id);
            sponsor.Deleted = true;
            db.Entry(sponsor).State = EntityState.Modified;
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
