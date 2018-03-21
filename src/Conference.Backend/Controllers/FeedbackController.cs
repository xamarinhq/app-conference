using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server;
using Conference.DataObjects;
using Conference.Backend.Models;
using Conference.Backend.Identity;
using Conference.Backend.Helpers;
using System.Net;
using System;
using System.Web.Http.OData;

namespace Conference.Backend.Controllers
{
    public class FeedbackController : TableController<Feedback>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            ConferenceContext context = new ConferenceContext();
            DomainManager = new EntityDomainManager<Feedback>(context, Request, true);
        }
        
        public IQueryable<Feedback> GetAllFeedback()
        {
            var items = Query();

            var email = EmailHelper.GetAuthenticatedUserEmail(RequestContext);

            var final = items.Where(feedback => feedback.UserId == email);

            return final;

        }


        [Authorize]
        public SingleResult<Feedback> GetFeedback(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        public Task<Feedback> PatchFeedback(string id, Delta<Feedback> patch)
        {
            return UpdateAsync(id, patch);
        }

        [Authorize]
        public async Task<IHttpActionResult> PostFeedback(Feedback item)
        {
            var feedback = item;
            feedback.UserId = EmailHelper.GetAuthenticatedUserEmail(RequestContext);

            var current = await InsertAsync(feedback);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }
    }
}