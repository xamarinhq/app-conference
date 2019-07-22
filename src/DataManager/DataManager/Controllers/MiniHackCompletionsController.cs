using DataManager.Helpers;
using DataManager.Repository;
using DataManager.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataManager.Controllers
{
    public class MiniHackCompletionsController : Controller
    {
        private LaComarcaRepository db = new LaComarcaRepository();

        const string queryName = "DataManager.Repository.Queries.MiniHackCompletions.sql";

        // GET: MiniHackCompletions
        public async Task<ActionResult> Index()
        {
            var query = await ResourceHelper.GetResourceString(queryName);
            var scores = await db.Database.SqlQuery<MiniHackCompletionViewModel>(query).ToListAsync();

            return View(scores);
        }
    }
}