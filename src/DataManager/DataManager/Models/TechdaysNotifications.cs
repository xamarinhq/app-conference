using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.NotificationHubs;

    namespace XamarinEvolve.Backend.Models
    {
        public class TechdaysNotifications
        {
            public static TechdaysNotifications Instance = new TechdaysNotifications();

            public NotificationHubClient Hub { get; }

            public TechdaysNotifications()
            {
                Hub = NotificationHubClient.CreateClientFromConnectionString(ConfigurationManager.AppSettings["HubConnection"], ConfigurationManager.AppSettings["HubEndpoint"]);
            }
        }
    }

}