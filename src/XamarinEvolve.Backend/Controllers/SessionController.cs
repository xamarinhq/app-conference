using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using XamarinEvolve.DataObjects;
using XamarinEvolve.Backend.Models;
using XamarinEvolve.Backend.Identity;
using XamarinEvolve.Backend.Helpers;

namespace XamarinEvolve.Backend.Controllers
{
    public class SessionController : TableController<Session>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinEvolveContext context = new XamarinEvolveContext();
            DomainManager = new EntityDomainManager<Session>(context, Request, true);
        }

        [QueryableExpand("Room,Speakers,MainCategory")]
        [EnableQuery(MaxTop=500)]
        public IQueryable<Session> GetAllSession()
        {
            return Query(); 
        }

        [QueryableExpand("Speakers,Room,MainCategory")]
        public SingleResult<Session> GetSession(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<Session> PatchSession(string id, Delta<Session> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostSession(Session item)
        {
            Session current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteSession(string id)
        {
             return DeleteAsync(id);
        }

    }
}