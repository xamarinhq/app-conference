using System;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using XamarinEvolve.Backend.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server.Config;
using System.Linq;

namespace XamarinEvolve.Backend.Controllers
{
    [MobileAppController]
    public class RegisterController : ApiController
    {
        private NotificationHubClient hub;
        public RegisterController()
        {
            hub = EvolveNotifications.Instance.Hub;
        }

        public class DeviceRegistration
        {
            public string Platform { get; set; }
            public string Handle { get; set; }
            public string[] Tags { get; set; }

        }

        //POST api/register
        //this creates a registration id
        public async Task<string> Post(string handle = null)
        {
            string newRegistrationId = null;
            if(handle != null)
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(handle, 100);
                foreach(var registration in registrations)
                {
                    if (newRegistrationId == null)
                        newRegistrationId = registration.RegistrationId;
                    else
                        await hub.DeleteRegistrationAsync(registration);
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await hub.CreateRegistrationIdAsync();

            return newRegistrationId;
        }

        // PUT api/put/5
        // This creates or updates a registration (with provided channelURI) at the specified id
        public async Task<HttpResponseMessage> Put(string id, DeviceRegistration deviceUpdate)
        {
            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "gcm":
                    registration = new GcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            registration.RegistrationId = id;
            var username = HttpContext.Current.User.Identity.Name;

            // add check if user is allowed to add these tags
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);
            registration.Tags.Add("username:" + username);

            try
            {
                await hub.CreateOrUpdateRegistrationAsync(registration);
            }
            catch (MessagingException e)
            {
                ReturnGoneIfHubResponseIsGone(e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/delete/5
        public async Task<HttpResponseMessage> Delete(string id)
        {
            await hub.DeleteRegistrationAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Get()
        {
            var ids = await hub.GetAllRegistrationsAsync(100);
            return Request.CreateResponse(HttpStatusCode.OK, ids.ToList());
        }

        public async Task<HttpResponseMessage> Get (string id)
        {

            var thisOne = await hub.GetRegistrationAsync<RegistrationDescription>(id);
            if (thisOne == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find id");

            return Request.CreateResponse(HttpStatusCode.OK, thisOne);
        }

        private static void ReturnGoneIfHubResponseIsGone(MessagingException e)
        {
            var webex = e.InnerException as WebException;
            if (webex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = (HttpWebResponse)webex.Response;
                if (response.StatusCode == HttpStatusCode.Gone)
                    throw new HttpRequestException(HttpStatusCode.Gone.ToString());
            }
        }
    }
}
