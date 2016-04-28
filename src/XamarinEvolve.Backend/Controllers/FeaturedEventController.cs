using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using XamarinEvolve.Backend.Models;
using XamarinEvolve.DataObjects;
using XamarinEvolve.Backend.Identity;
using XamarinEvolve.Backend.Helpers;

namespace XamarinEvolve.Backend.Controllers
{
    public class FeaturedEventController : TableController<FeaturedEvent>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinEvolveContext context = new XamarinEvolveContext();
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
