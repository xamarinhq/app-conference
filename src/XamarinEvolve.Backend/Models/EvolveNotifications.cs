using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace XamarinEvolve.Backend.Models
{
    public class EvolveNotifications
    {
        public static EvolveNotifications Instance = new EvolveNotifications();

        public NotificationHubClient Hub { get; }

        public EvolveNotifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString(ConfigurationManager.AppSettings["HubConnection"], ConfigurationManager.AppSettings["HubEndpiont"]);
        }
    }
}
