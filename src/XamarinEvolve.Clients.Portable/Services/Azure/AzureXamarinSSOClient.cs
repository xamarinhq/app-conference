using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using XamarinEvolve.DataStore.Abstractions;
using XamarinEvolve.DataStore.Azure;

namespace XamarinEvolve.Clients.Portable.Auth.Azure
{
    public sealed class XamarinSSOClient : ISSOClient
    {
        private readonly StoreManager storeManager;
     
        public XamarinSSOClient(StoreManager storeManager)
        {
            if (storeManager == null)
            {
                throw new ArgumentNullException(nameof(storeManager));
            }

            this.storeManager = storeManager;
        }

        public XamarinSSOClient()
        {
            storeManager = DependencyService.Get<IStoreManager>() as StoreManager;

            if (storeManager == null)
            {
                throw new InvalidOperationException($"The {typeof(XamarinSSOClient).FullName} requires a {typeof(StoreManager).FullName}.");
            }
        }

        public async Task<AccountResponse> LoginAsync(string username, string password)
        {
            MobileServiceUser user = await storeManager.LoginAsync(username, password);

            return AccountFromMobileServiceUser(user);
        }

        public async Task LogoutAsync()
        {
            await storeManager.LogoutAsync();
        }

        private AccountResponse AccountFromMobileServiceUser(MobileServiceUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            IDictionary<string, string> claims = JwtUtility.GetClaims(user.MobileServiceAuthenticationToken);

            var account = new AccountResponse();
            account.Success = true;
            account.User = new User
            {
                Email = claims[JwtClaimNames.Subject],
                FirstName = claims[JwtClaimNames.GivenName],
                LastName = claims[JwtClaimNames.FamilyName]
            };

            return account;
        }
    }
}
