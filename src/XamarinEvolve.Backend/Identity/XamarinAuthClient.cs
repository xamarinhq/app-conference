using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XamarinEvolve.Backend.Identity
{
    public sealed class XamarinAuthClient : IDisposable
    {
        private readonly HttpClient client;

        public XamarinAuthClient(string apiKey)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            client = new HttpClient
            {
                BaseAddress = new Uri("https://auth.xamarin.com/api/v1/")
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        ~XamarinAuthClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                client.Dispose();
            }
        }

        public async Task<XamarinAuthResponse> GetAuthenticationTokenAsync(XamarinCredentials credentials)
        {
            var content = new Dictionary<string, string>
            {
                { "email", credentials.Email },
                { "password", credentials.Password }
            };

            var response = await client.PostAsync("auth", new FormUrlEncodedContent(content));

            return await response.Content.ReadAsAsync<XamarinAuthResponse>();
        }
    }
}
