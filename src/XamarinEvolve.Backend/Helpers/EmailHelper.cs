using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;

namespace XamarinEvolve.Backend.Helpers
{
    public class EmailHelper
    {
        public static string GetAuthenticatedUserEmail()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claims = identity.Claims;

            var claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

            return (from c in claims
                    where c.Type == claimType
                    select c.Value).Single();
        }

        public static string GetAuthenticatedUserEmail(HttpRequestContext context)
        {
            var identity = (ClaimsIdentity)context.Principal.Identity;
            var claims = identity.Claims;

            var claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

            return (from c in claims
                    where c.Type == claimType
                    select c.Value).Single();
        }
    }
}