using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Login;
using Newtonsoft.Json.Linq;
using XamarinEvolve.Backend.Identity;

namespace XamarinEvolve.Backend.Controllers
{
    public class XamarinAuthController : ApiController
    {
        private const string XamarinAuthApiKeyName = "XamarinAuthApiKey";
        private const string AuthSigningKeyVariableName = "WEBSITE_AUTH_SIGNING_KEY";
        private const string HostNameVariableName = "WEBSITE_HOSTNAME";

        public async Task<IHttpActionResult> Post([FromBody] JObject assertion)
        {
            XamarinAuthResponse authResult = await AuthenticateCredentials(assertion.ToObject<XamarinCredentials>());

            if (!authResult.Success)
            {
                return Unauthorized();
            }

            IEnumerable<Claim> claims = GetAccountClaims(authResult.User);
            string websiteUri = $"https://{WebsiteHostName}/";

            JwtSecurityToken token = AppServiceLoginHandler.CreateToken(claims, TokenSigningKey, websiteUri, websiteUri, TimeSpan.FromDays(10));

            return Ok(new LoginResult { RawToken = token.RawData, User = new User { UserId = authResult.User.Email } });
        }

        private IEnumerable<Claim> GetAccountClaims(XamarinUser user) => new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            };

        private async Task<XamarinAuthResponse> AuthenticateCredentials(XamarinCredentials credentials)
        {
            string authApiKey = ConfigurationManager.AppSettings[XamarinAuthApiKeyName];

            if (authApiKey == null)
            {
                throw new InvalidOperationException("Missing XamarinAuthApiKey configuration setting.");
            }

            using (var authClient = new XamarinAuthClient(authApiKey))
            {
                return await authClient.GetAuthenticationTokenAsync(credentials);
            }
        }

        private string TokenSigningKey => Environment.GetEnvironmentVariable(AuthSigningKeyVariableName) ?? "test_key";

        public string WebsiteHostName => Environment.GetEnvironmentVariable(HostNameVariableName) ?? "localhost";
    }
}
