using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DataManager.Models;
using DataManager.Repository;

namespace DataManager.Controllers
{
        public class AnnouncementController : ApiController
        {
            // POST api/Announcement

            public async Task<HttpResponseMessage> Post(string password, [FromBody]string message)
            {
                if (string.IsNullOrWhiteSpace(message) || password != ConfigurationManager.AppSettings["NotificationsPassword"])
                    return Request.CreateResponse(HttpStatusCode.Forbidden);

                try
                {
                    var accounenement = new Notification
                    {
                        Date = DateTime.UtcNow,
                        Text = message,
                        Id = Guid.NewGuid().ToString()
                    };

                    var context = new LaComarcaRepository();

                    context.Notifications.Add(accounenement);

                    await context.SaveChangesAsync();

                }
                catch
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
 
}
