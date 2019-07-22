using DataManager.Helpers;
using DataManager.Repository;
using DataManager.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataManager.Controllers
{
    public class SessionRatingsController : Controller
    {
        private LaComarcaRepository db = new LaComarcaRepository();

        const string queryName = "DataManager.Repository.Queries.SessionWithRatings.sql";

        // GET: SessionRatings
        public async Task<ActionResult> Index()
        {
            var query = await ResourceHelper.GetResourceString(queryName);
            var scores = await db.Database.SqlQuery<SessionWithRatingsViewModel>(query).ToListAsync();

            return View(scores);
        }
    }
}