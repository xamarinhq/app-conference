using DataManager.Helpers;
using DataManager.Repository;
using DataManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataManager.Controllers
{
    [Authorize]
    public class TopSessionsController : Controller
    {
        private LaComarcaRepository db = new LaComarcaRepository();

        const string queryName = "DataManager.Repository.Queries.TopSessions.sql";

        // GET: TopSessions
        public async Task<ActionResult> Index()
        {
            var query = await ResourceHelper.GetResourceString(queryName);
            var sessions = await db.Database.SqlQuery<SessionWithFavoritesViewModel>(query).ToListAsync();
            return View(sessions);
        }
    }
}