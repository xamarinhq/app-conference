using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Conference.Backend.Models;
using Conference.DataObjects;
using Newtonsoft.Json;

namespace Conference.Backend.DefaultData
{
    public class DefaultDataSetup
    {
        private static List<T> Get<T>(string name)
        {
            var fileName = string.Format("~/DefaultData/Json/{0}.json", name);
            var jsonFile = HttpContext.Current.Server.MapPath(fileName);

            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonFile));
        }

        public static void AddAll(ConferenceContext context)
        {
            // Categories
            var categories = Get<Category>("Categories");
            categories.ForEach(category => context.Categories.Add(category));

            var sponsorLevels = Get<SponsorLevel>("SponsorLevels");
            sponsorLevels.ForEach(sponsorLevel => context.SponsorLevels.Add(sponsorLevel));

            var sponsors = Get<Sponsor>("Sponsors");
            sponsors.ForEach(sponsor => {
                var sponsorLevel = context.SponsorLevels.Find(sponsor.SponsorLevel.Id);
                sponsor.SponsorLevel = sponsorLevel;
                context.Sponsors.Add(sponsor);
            });

            //var miniHacks = Get<MiniHack>("MiniHacks");
            //miniHacks.ForEach(miniHack => context.MiniHacks.Add(miniHack));

            var featuredEvents = Get<FeaturedEvent>("FeaturedEvents");
            featuredEvents.ForEach(featuredEvent =>
            {

                if (featuredEvent.Sponsors != null)
                {
                    foreach (var sponsor in featuredEvent.Sponsors)
                    {
                        if (sponsor != null)
                        {
                            var validSponsor = context.Sponsors.Find(sponsor.Id);

                            if(validSponsor != null)
                                featuredEvent.Sponsors.Add(validSponsor);
                        }
                    }
                }

                context.FeaturedEvents.Add(featuredEvent);
            });

            var speakers = Get<Speaker>("Speakers");
            speakers.ForEach(speaker => context.Speakers.Add(speaker));

            var rooms = Get<Room>("Rooms");
            rooms.ForEach(room => context.Rooms.Add(room));


            var sessions = Get<Session>("Sessions");
            foreach (var session in sessions)
            {
                var newSession = session;
                newSession.Speakers = new List<Speaker>();

                if (session != null)
                {
                    var validEvent = context.FeaturedEvents.Where(e => e.Id == session.EventID).Single();

                    if (validEvent == null)
                        continue;

                    foreach (var speacker in session.Speakers)
                    {
                        var validSpeaker = context.Speakers.Where(s => s.Id == speacker.Id).Single();

                        if (validSpeaker != null)
                            newSession.Speakers.Add(validSpeaker);
                    }

                    newSession.Room = context.Rooms.Find(newSession.Room.Id);
                    newSession.MainCategory = context.Categories.Find(newSession.MainCategory.Id);

                    context.Sessions.Add(newSession);
                } 
            }
           

        }
    }
}