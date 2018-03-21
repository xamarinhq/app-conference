using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Conference.Backend.Models;
using Conference.DataObjects;
using Conference.Backend.Identity;
using Conference.Backend.Helpers;

namespace Conference.Backend.Controllers
{
    public class FeaturedEventController : TableController<FeaturedEvent>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ConferenceContext context = new ConferenceContext();
            DomainManager = new EntityDomainManager<FeaturedEvent>(context, Request, true);
        }

        [QueryableExpand("Sponsor")]
        public IQueryable<FeaturedEvent> GetAllFeaturedEvent()
        {
            return Query(); 
        }

        [QueryableExpand("Sponsor")]
        public SingleResult<FeaturedEvent> GetFeaturedEvent(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<FeaturedEvent> PatchFeaturedEvent(string id, Delta<FeaturedEvent> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostFeaturedEvent(FeaturedEvent item)
        {
            FeaturedEvent current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteFeaturedEvent(string id)
        {
             return DeleteAsync(id);
        }
    }
}
