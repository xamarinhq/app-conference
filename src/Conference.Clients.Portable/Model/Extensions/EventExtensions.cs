using System;

using Xamarin.Forms;
using MvvmHelpers;
using System.Collections.Generic;
using System.Linq;
using Conference.DataObjects;

namespace Conference.Clients.Portable
{
    public static class EventExtensions 
    {
        public static IEnumerable<Grouping<string, FeaturedEvent>> GroupByDate(this IEnumerable<FeaturedEvent> events)
        {
            return from e in events
                orderby e.StartTimeOrderBy
                group e by e.GetSortName()
                into eventGroup
                select new Grouping<string, FeaturedEvent>(eventGroup.Key, eventGroup); 
        }

        public static string GetSortName(this FeaturedEvent e)
        {
            if (!e.StartTime.HasValue || !e.EndTime.HasValue)
                return "TBA";
            
            var start = e.StartTime.Value.ToEasternTimeZone();

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow";
            }
            var monthDay = start.ToString("M");
            return $"{monthDay}";
        }

        public static string GetDisplayName(this FeaturedEvent e)
        {
            

            if (!e.StartTime.HasValue || !e.EndTime.HasValue || e.StartTime.Value.IsTBA())
                return "To be announced";

            var start = e.StartTime.Value.ToEasternTimeZone();

            if (e.IsAllDay)
                return "All Day";

            var startString = start.ToString("t"); 
            var end = e.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today {startString}–{endString}";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}–{endString}";
            }

            var day = start.DayOfWeek.ToString();
            var monthDay = start.ToString("M");
            return $"{day}, {monthDay}, {startString}–{endString}";
        }


      

        public static string GetDisplayTime(this FeaturedEvent e)
        {
           
            if (!e.StartTime.HasValue || !e.EndTime.HasValue || e.StartTime.Value.IsTBA())
                return "To be announced";

            var start = e.StartTime.Value.ToEasternTimeZone();


            if (e.IsAllDay)
                return "All Day";

            var startString = start.ToString("t"); 
            var end = e.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");

            return $"{startString}–{endString}";
        }
    }
}


