using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace XamarinEvolve.Backend.Identity
{
    public class EmployeeAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claims = identity.Claims;
        
            const string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            var email = (from c in claims
                         where c.Type == claimType
                         select c.Value).Single();

            // If not already authenticated, return.
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // If they don't have an identity name at all, return.
            if (string.IsNullOrEmpty(email))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            
            var address = IsValidEmail(email);

            // If their name is not a valid email, return.
            if (address == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // If user email does not contain xamarin.com, return.
            if (!address.Host.ToLower().Equals(ConfigurationManager.AppSettings["CompanyDomain"]))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        MailAddress IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr;
            }
            catch
            {
                return null;
            }
        }
    }
}