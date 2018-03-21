using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Conference.DataObjects;
using Conference.Backend.Models;
using Conference.Backend.Identity;
using Conference.Backend.Helpers;

namespace Conference.Backend.Controllers
{
    public class SessionController : TableController<Session>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ConferenceContext context = new ConferenceContext();
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