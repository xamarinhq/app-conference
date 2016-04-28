using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using XamarinEvolve.DataObjects;
using XamarinEvolve.Backend.Models;
using XamarinEvolve.Backend.Identity;
using XamarinEvolve.Backend.Helpers;
using System.Net;
using System.Web.Http.OData;

namespace XamarinEvolve.Backend.Controllers
{
    public class FavoriteController : TableController<Favorite>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinEvolveContext context = new XamarinEvolveContext();
            DomainManager = new EntityDomainManager<Favorite>(context, Request, true);
            
        }

        public IQueryable<Favorite> GetAllFavorite()
        {
            var items = Query();
            var email = EmailHelper.GetAuthenticatedUserEmail(RequestContext);

            var final = items.Where(favorite => favorite.UserId == email);

            return final;
        }

        [Authorize]
        public SingleResult<Favorite> GetFavorite(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        public Task<Favorite> PatchFavorite(string id, Delta<Favorite> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        public async Task<IHttpActionResult> PostFavorite(Favorite item)
        {
            item.UserId = EmailHelper.GetAuthenticatedUserEmail(RequestContext);

            var current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [Authorize]
        public Task DeleteFavorite(string id)
        {
            return DeleteAsync(id);
        }

    }
}