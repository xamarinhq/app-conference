using System;
using FormsToolkit;

namespace Conference.Clients.Portable
{
    public static class MessagingUtils
    {
        public static void SendOfflineMessage()
        {
            MessagingService.Current.SendMessage(MessageKeys.Message, new MessagingServiceAlert
                {
                    Title="Offline",
                    Message="You are currently offline, please connect to the internet and try again.",
                    Cancel="OK"
                });
        }

        public static void SendAlert(string title, string message)
        {
            MessagingService.Current.SendMessage(MessageKeys.Message, new MessagingServiceAlert
                {
                    Title=title,
                    Message=message,
                    Cancel="OK"
                });
        }
    }
}

