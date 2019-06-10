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
    public class SessionsController : Controller
    {
        private Techdays2016Repository db = new Techdays2016Repository();

        // GET: Sessions
        public async Task<ActionResult> Index()
        {
            //var sessions = await db.Sessions.OrderBy(s => s.StartTime).
            //    OrderBy(s => s.Title).Include("Speakers").ToListAsync();

            var sessions = await db.Sessions.Where(s=>s.Deleted==false).Include("Speakers").Include("Room")
                .OrderBy(s => s.StartTime==null).ThenBy(s=>s.StartTime).ToListAsync();
               

   
            return View(sessions);
        }

        // GET: Sessions/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            await db.Entry(session).Collection(s => s.Speakers).LoadAsync();
            await db.Entry(session).Collection(c => c.Categories).LoadAsync();

            return View(session);
        }

        // GET: Sessions/Create
        public async Task<ActionResult> Create()
        {
            SessionViewModel model = await CreateViewmodel();
            return View(model);
        }



        private async Task<SessionViewModel> CreateViewmodel(Session session = null)
        {
            if (session == null)
                session = new Session();
            var model = new SessionViewModel();

            model.TrackedSession = session;
            model.AvailableCategories = await db.Categories.OrderBy(c => c.Name).ToListAsync();
            
     
            model.AvailableRooms = await db.Rooms.OrderBy(r => r.Name).ToListAsync();

     
            model.AvailableSpeakers = await db.Speakers.OrderBy(s => s.FirstName).ToListAsync();
            model.AvailableLevels = new List<string>() { "100", "200", "300", "400" };
            model.SubmittedCategories = new List<string>();
            model.SubmittedRoom = "";
            model.SubmittedSpeakers = new List<string>();
   
       
            return model;
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SessionViewModel session)
        {


            if (ModelState.IsValid)
            {
                session.TrackedSession.Id = Guid.NewGuid().ToString();
                session.TrackedSession.Speakers = await GetSpeakersForIDs(session.SubmittedSpeakers);
                session.TrackedSession.Categories = await GetCategoriesForIds(session.SubmittedCategories);
                session.TrackedSession.Room = await GetRoomForId(session.SubmittedRoom);
                session.TrackedSession.Level = session.submittedLevel;

                db.Sessions.Add(session.TrackedSession);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var model = await CreateViewmodel(session.TrackedSession);

            return View(session);
        }
        
        private async Task<Room> GetRoomForId(string submittedRoom)
        {
            var room = await db.Rooms.FindAsync(submittedRoom);
            return room;
        }

        private async Task<ICollection<Category>> GetCategoriesForIds(List<string> submittedCategories)
        {
            var categories = await db.Categories.Where(category => submittedCategories.Contains(category.Id)).ToListAsync();

            return categories;
        }

        private async Task<ICollection<Speaker>> GetSpeakersForIDs(List<string> submittedSpeakers)
        {
            var speakers = await db.Speakers.Where(speaker => submittedSpeakers.Contains(speaker.Id)).ToListAsync();

            return speakers;
        }

        // GET: Sessions/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var session = await db.Sessions.Include("Room").Where(s=>s.Id==id).FirstOrDefaultAsync();

            if (session == null)
            {
                return HttpNotFound();
            }
            await db.Entry(session).Collection(s => s.Speakers).LoadAsync();
            await db.Entry(session).Collection(c => c.Categories).LoadAsync();
            var room = session.Room;
            var model = await CreateViewmodel(session);

            return View(model);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SessionViewModel session)
        {

            if (ModelState.IsValid)
            {
                var sessionToUpdate = db.Sessions.Include("Room")
                    .Include("Speakers")
                    .Include("Categories")
                    .Where(s => s.Id == session.TrackedSession.Id).Single();
          
                var submittedSpeakers = await GetSpeakersForIDs(session.SubmittedSpeakers);
                UpdateSessionSpeakers(sessionToUpdate, submittedSpeakers);


                var submittedCategories = await GetCategoriesForIds(session.SubmittedCategories);
                UpdateSessionCategories(sessionToUpdate, submittedCategories);

                var submittedRoom = await GetRoomForId(session.SubmittedRoom);
                sessionToUpdate.Room = submittedRoom;

                sessionToUpdate.Abstract = session.TrackedSession.Abstract;
                sessionToUpdate.Title = session.TrackedSession.Title;
                sessionToUpdate.ShortTitle = session.TrackedSession.ShortTitle;
                sessionToUpdate.Level = session.submittedLevel;
                sessionToUpdate.StartTime = session.TrackedSession.StartTime;
                sessionToUpdate.EndTime = session.TrackedSession.EndTime;
                sessionToUpdate.PresentationUrl = session.TrackedSession.PresentationUrl;
                sessionToUpdate.VideoUrl = session.TrackedSession.VideoUrl;

                db.Entry(sessionToUpdate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(session);
        }

        private void UpdateSessionCategories(Session session, ICollection<Category> submittedCategories)
        {
            foreach (var category in submittedCategories)
            {
                if (!session.Categories.Contains(category))
                {
                    session.Categories.Add(category);
                }
            }

            var itemsToRemove = session.Categories.Where(c => !submittedCategories.Contains(c)).ToArray();
            foreach (var category in itemsToRemove)
            {
                session.Categories.Remove(category);
            }

        }

        private static void UpdateSessionSpeakers(Session session, ICollection<Speaker> submittedSpeakers)
        {
            foreach (var speaker in submittedSpeakers)
            {
                if (!session.Speakers.Contains(speaker))
                {
                    session.Speakers.Add(speaker);
                }
            }

            var itemsToRemove = session.Speakers.Where(s => !submittedSpeakers.Contains(s)).ToArray();
            foreach (var speaker in itemsToRemove)
            {
                session.Speakers.Remove(speaker);
            }
        }

        // GET: Sessions/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);

            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Session session = await db.Sessions.FindAsync(id);
            session.Deleted = true;
            db.Entry(session).State = EntityState.Modified;
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
