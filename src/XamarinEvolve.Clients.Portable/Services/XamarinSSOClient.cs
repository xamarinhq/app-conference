using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;

namespace XamarinEvolve.Clients.Portable.Auth
{
    public class XamarinSSOClient : ISSOClient
    {
        const string XamarinSSOApiKey = "0c833t3w37jq58dj249dt675a465k6b0rz090zl3jpoa9jw8vz7y6awpj5ox0qmb";

        readonly HttpClient client;

        public XamarinSSOClient()
            : this(XamarinSSOApiKey)
        {
        }

        public XamarinSSOClient(string apiKey)
        {
            client = new HttpClient
                {
                    BaseAddress = new Uri("https://auth.xamarin.com/api/v1/")
                };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        async Task<string> PostForm(string endpoint, IDictionary<string,string> keyValues)
        {
            var response = await client.PostAsync(endpoint, new FormUrlEncodedContent(keyValues));
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<AccountResponse> CreateToken(string email, string password)
        {
            var json = await PostForm("auth", new Dictionary<string,string>
                {
                    {"email", email},
                    {"password", password},
                });
            return JsonConvert.DeserializeObject<AccountResponse>(json);
        }

        #region ISSOClient implementation

        public async Task<AccountResponse> LoginAsync(string username, string password) =>
            await CreateToken(username, password);

        public Task LogoutAsync()
        {
            return Task.FromResult(0);
        }


        #endregion
    }
}