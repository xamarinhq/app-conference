using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Conference.Backend.Models;
using Conference.DataObjects;
using Conference.Backend.Identity;

namespace Conference.Backend.Controllers
{
    public class MiniHackController : TableController<MiniHack>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ConferenceContext context = new ConferenceContext();
            DomainManager = new EntityDomainManager<MiniHack>(context, Request, true);
        }

        public IQueryable<MiniHack> GetAllMiniHack()
        {
            return Query(); 
        }

        public SingleResult<MiniHack> GetMiniHack(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<MiniHack> PatchMiniHack(string id, Delta<MiniHack> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostMiniHack(MiniHack item)
        {
            MiniHack current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteMiniHack(string id)
        {
             return DeleteAsync(id);
        }
    }
}
