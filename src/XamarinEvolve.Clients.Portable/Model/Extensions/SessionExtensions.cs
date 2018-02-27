using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using XamarinEvolve.DataObjects;
using NodaTime;
using Xamarin.Forms;
using XamarinEvolve.DataStore.Abstractions;
using Humanizer;
using System.Threading.Tasks;

namespace XamarinEvolve.Clients.Portable
{
    public static class SessionStateExtensions
    {
        public static AppLinkEntry GetAppLink(this Session session)
        {
            var url = $"http://evolve.xamarin.com/session/{session.RemoteId.ToString()}";
            
            var entry = new AppLinkEntry
            {
                Title = session.Title,
                Description = session.Abstract,
                AppLinkUri = new Uri(url, UriKind.RelativeOrAbsolute),
                IsLinkActive = true
            };

            if (Device.RuntimePlatform == Device.iOS)
                entry.Thumbnail = ImageSource.FromFile("Icon.png");

            entry.KeyValues.Add("contentType", "Session");
            entry.KeyValues.Add("appName", "Evolve16");
            entry.KeyValues.Add("companyName", "Xamarin");

            return entry;
        }

        public static string GetIndexName(this Session e)
        {
            if(!e.StartTime.HasValue || !e.EndTime.HasValue || e.StartTime.Value.IsTBA())
                return "To be announced";
            
            var start = e.StartTime.Value.ToEasternTimeZone();


            var startString = start.ToString("t"); 
            var end = e.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");

            var day = start.DayOfWeek.ToString();
            var monthDay = start.ToString("M");
            return $"{day}, {monthDay}, {startString}–{endString}";
        }

        public static string GetSortName(this Session session)
        {

            if (!session.StartTime.HasValue || !session.EndTime.HasValue  || session.StartTime.Value.IsTBA())
                return "To be announced";
            
            var start = session.StartTime.Value.ToEasternTimeZone();
            var startString = start.ToString("t"); 

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today {startString}";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}";
            }
            var day = start.ToString("M");
            return $"{day}, {startString}";
        }

        public static string GetDisplayName(this Session session)
        {
            if (!session.StartTime.HasValue || !session.EndTime.HasValue || session.StartTime.Value.IsTBA())
                return "TBA";
            
            var start = session.StartTime.Value.ToEasternTimeZone();
            var startString = start.ToString("t"); 
            var end = session.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");

           

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today {startString}–{endString}";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}–{endString}";
            }
            var day = start.ToString("M");
            return $"{day}, {startString}–{endString}";
        }


        public static string GetDisplayTime(this Session session)
        {
            if (!session.StartTime.HasValue || !session.EndTime.HasValue || session.StartTime.Value.IsTBA())
                return "TBA";
            var start = session.StartTime.Value.ToEasternTimeZone();


            var startString = start.ToString("t"); 
            var end = session.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");
            return $"{startString}–{endString}";
        }


        public static IEnumerable<Grouping<string, Session>> FilterAndGroupByDate(this IList<Session> sessions)
        {
            if (Settings.Current.FavoritesOnly)
            {
                sessions = sessions.Where(s => s.IsFavorite).ToList();
            }

            var tba = sessions.Where(s => !s.StartTime.HasValue || !s.EndTime.HasValue || s.StartTime.Value.IsTBA());


            var showPast = Settings.Current.ShowPastSessions;
            var showAllCategories = Settings.Current.ShowAllCategories;
            var filteredCategories = Settings.Current.FilteredCategories;
            var utc = DateTime.UtcNow;


            //is not tba
            //has not started or has started and hasn't ended or ended 20 minutes ago
            //filter then by category and filters
            var grouped = (from session in sessions
                where session.StartTime.HasValue && session.EndTime.HasValue && !session.StartTime.Value.IsTBA() && (showPast || (utc <= session.StartTime.Value || utc <= session.EndTime.Value.AddMinutes(20))) 
                && (showAllCategories || filteredCategories.IndexOf(session?.MainCategory?.Name ?? string.Empty, StringComparison.OrdinalIgnoreCase) >= 0)
                            orderby session.StartTimeOrderBy, session.Title
                            group session by session.GetSortName()
                            into sessionGroup
                            select new Grouping<string, Session>(sessionGroup.Key, sessionGroup)).ToList(); 

            if (tba.Any())
                grouped.Add(new Grouping<string, Session>("TBA", tba));

            return grouped;
        }

        public static IEnumerable<Session> Search(this IEnumerable<Session> sessions, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return sessions;

            var searchSplit = searchText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //search title, then category, then speaker name
            return sessions.Where(session => 
                                  searchSplit.Any(search => 
                                session.Haystack.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
        }
    }
}

