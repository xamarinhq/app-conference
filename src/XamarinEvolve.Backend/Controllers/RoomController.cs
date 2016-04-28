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
    public class RoomController : TableController<Room>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            XamarinEvolveContext context = new XamarinEvolveContext();
            DomainManager = new EntityDomainManager<Room>(context, Request, true);
        }

        public IQueryable<Room> GetAllRoom()
        {
            return Query();
        }

        public SingleResult<Room> GetRoom(string id)
        {
            return Lookup(id);
        }

        [EmployeeAuthorize]
        public Task<Room> PatchRoom(string id, Delta<Room> patch)
        {
             return UpdateAsync(id, patch);
        }

        [EmployeeAuthorize]
        public async Task<IHttpActionResult> PostRoom(Room item)
        {
            Room current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [EmployeeAuthorize]
        public Task DeleteRoom(string id)
        {
             return DeleteAsync(id);
        }

    }
}