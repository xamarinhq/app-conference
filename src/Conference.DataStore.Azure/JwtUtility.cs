using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Conference.DataStore.Azure
{
    public static class JwtUtility
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static IDictionary<string, string> GetClaims(string rawToken)
        {
            var tokenParts = rawToken.Split('.');

            if (tokenParts.Length != 3)
            {
                throw new InvalidTokenException("Invalid user token");
            }

            var mobileAppToken = GetDecodedPayload(tokenParts[1]);

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(mobileAppToken);
        }

        public static DateTime? GetTokenExpiration(string token)
        {
            var claims = GetClaims(token);

            if (claims.ContainsKey(JwtClaimNames.Expiration))
            {
                var secondsFromEpoch = int.Parse(claims[JwtClaimNames.Expiration]);

                return (UnixEpoch + TimeSpan.FromSeconds(secondsFromEpoch)).ToUniversalTime();
            }

            return null;
        }

        private static string GetDecodedPayload(string tokenPayload)
        {
            if (tokenPayload.Length % 4 == 1)
            {
                throw new InvalidTokenException("Invalid user token");
            }

            //Replace 62nd and 63rd char of encoding
            tokenPayload = tokenPayload.Replace("-", "+");
            tokenPayload = tokenPayload.Replace("_", "/");

            // Add padding:
            var padding = 4 - (tokenPayload.Length % 4);
            tokenPayload = tokenPayload.PadRight(tokenPayload.Length + (padding % 4), '=');

            var tokenPayloadBytes = Convert.FromBase64String(tokenPayload);

            return Encoding.UTF8.GetString(tokenPayloadBytes, 0, tokenPayloadBytes.Length);
        }
    }
}
