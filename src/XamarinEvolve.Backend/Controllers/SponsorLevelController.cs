using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using XamarinEvolve.DataObjects;
using XamarinEvolve.Backend.Models;
using XamarinEvolve.Backend.Identity;

namespace XamarinEvolve.Backend.Controllers
{
    public class SponsorLevelController : TableController<SponsorLevel>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinEvolveContext context = new XamarinEvolveContext();
            DomainManager = new EntityDomainManager<SponsorLevel>(context, Request, true);
        }

        public IQueryable<SponsorLevel> GetAllSponsorLevel()
        {
            return Query(); 
        }

        public SingleResult<SponsorLevel> GetSponsorLevel(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<SponsorLevel> PatchSponsorLevel(string id, Delta<SponsorLevel> patch)
        {
             return UpdateAsync(id, patch);
        }


        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostSponsorLevel(SponsorLevel item)
        {
            SponsorLevel current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteSponsorLevel(string id)
        {
             return DeleteAsync(id);
        }

    }
}