using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace Conference.Backend.Models
{
    public class ConferenceNotifications
    {
        public static ConferenceNotifications Instance = new ConferenceNotifications();

        public NotificationHubClient Hub { get; }

        public ConferenceNotifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString(ConfigurationManager.AppSettings["HubConnection"], ConfigurationManager.AppSettings["HubEndpiont"]);
        }
    }
}
