using System;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace Conference.DataStore.Mock
{
    public class EventStore : BaseStore<FeaturedEvent>, IEventStore
    {
        List<FeaturedEvent> Events { get; }
        ISponsorStore sponsors;
        public EventStore()
        {
            Events = new List<FeaturedEvent>();
            sponsors = DependencyService.Get<ISponsorStore>();
        }

        public override async Task InitializeStore()
        {
            if (Events.Count != 0)
                return;

            var sponsorList = await sponsors.GetItemsAsync();

            
            Events.Add(new FeaturedEvent
                {
                    Title = "Registration for Training & System Config",
                    Description = "Get ready for Conference training with open registration and full system configuration prep throughout the day!",
                    StartTime = new DateTime(2016, 4, 24, 16, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    LocationName = "Registration",
                    IsAllDay = false,
                });
            
            Events.Add(new FeaturedEvent
                {
                    Title = "Training Keynote",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 25, 1, 30, 0, DateTimeKind.Utc),
                    LocationName = "General Session",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Breakfast",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 25, 11, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 25, 13, 00, 0, DateTimeKind.Utc),
                    LocationName = "Meals",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Training Day 1",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 25, 13, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 25, 22, 00, 0, DateTimeKind.Utc),
                    LocationName = "Training Breakouts",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Evening Event",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 25, 23, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 26, 1, 0, 0, DateTimeKind.Utc),
                    LocationName = string.Empty,
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Breakfast",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 26, 11, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 26, 13, 0, 0, DateTimeKind.Utc),
                    LocationName = "Meals",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Training Day 2",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 26, 13, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 26, 22, 0, 0, DateTimeKind.Utc),
                    LocationName = "Training Breakouts",
                    IsAllDay = false,
                });

            Events.Add (new FeaturedEvent {
                Title = "Darwin Lounge",
                Description = "Stocked full of tech toys, massage chairs, Xamarin engineers, and a few really cool surprises, the Darwin Lounge is your place to hang out and relax between sessions. Back by popular demand, we’ll have several code challenges—Mini-Hacks—for you to complete to earn awesome prizes.",
                StartTime = new DateTime (2016, 4, 26, 23, 00, 0, DateTimeKind.Utc),
                EndTime = new DateTime (2016, 4, 27, 4, 0, 0, DateTimeKind.Utc),
                LocationName = "Darwin Lounge",
                IsAllDay = false,
            });


            Events.Add(new FeaturedEvent
                {
                    Title = "Conference Registration",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 26, 13, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 26, 23, 0, 0, DateTimeKind.Utc),
                    LocationName = "Registration",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Evening Event",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 26, 23, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 27, 1, 0, 0, DateTimeKind.Utc),
                LocationName = string.Empty,
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Breakfast",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 27, 12, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 27, 13, 0, 0, DateTimeKind.Utc),
                    LocationName = "Meals",
                    IsAllDay = false,
                });


            Events.Add(new FeaturedEvent
                {
                    Title = "Conference Keynote",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 27, 13, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 27, 14, 30, 0, DateTimeKind.Utc),
                    LocationName = "General Session",
                    IsAllDay = false,
                });

            Events.Add(new FeaturedEvent
                {
                    Title = "Happy Hour",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 27, 22, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 28, 0, 0, 0, DateTimeKind.Utc),
                    LocationName = "Expo Hall",
                    IsAllDay = false,
                    Sponsor = sponsorList.FirstOrDefault(x => x.Name == "Microsoft")
                });


            Events.Add (new FeaturedEvent {
                Title = "Darwin Lounge",
                Description = "Stocked full of tech toys, massage chairs, Xamarin engineers, and a few really cool surprises, the Darwin Lounge is your place to hang out and relax between sessions. Back by popular demand, we’ll have several code challenges—Mini-Hacks—for you to complete to earn awesome prizes.",
                StartTime = new DateTime (2016, 4, 27, 12, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime (2016, 4, 28, 0, 0, 0, DateTimeKind.Utc),
                LocationName = "Darwin Lounge",
                IsAllDay = false,
            });

            Events.Add (new FeaturedEvent {
                Title = "Conference Party",
                Description = "No lines, just fun! Conference 2016 is throwing an unforgettable celebration at Universal's Island of Adventure® theme park on Wednesday, April 27th. The Wizarding just for you! All night long, you'll enjoy unlimited access to incredible rides and attractions including Harry Potter and the Forbidden Journey™ and the Jurassic Park River Adventure®. It's an entire evening of thrills and excitement – and it's all yours!\n\nThere will be plenty of food, music and entertainment for those that prefer to keep their feet on dry land. You won't want to miss this exclusive event at one of Orlando's most famous theme parks!\n\nHARRY POTTER characters, names and related indicia are © & ™ Warner Bros. Entertainment Inc. Harry Potter Publishing Rights © JKR. (s16) Jurassic Park, Jurassic Park River Adventure® Universal Studios/Amblin. Universal elements and all related indicia TM & © 2016 Universal Studios. All rights reserved.",
                StartTime = new DateTime (2016, 4, 28, 0, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime (2016, 4, 28, 4, 0, 0, DateTimeKind.Utc),
                LocationName = "Universal's Island of Adventure®",
                IsAllDay = false,
            });


            Events.Add(new FeaturedEvent
                {
                    Title = "Breakfast",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 28, 12, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 28, 13, 0, 0, DateTimeKind.Utc),
                    LocationName = "Meals",
                    IsAllDay = false,
                });

            Events.Add (new FeaturedEvent {
                Title = "Darwin Lounge",
                Description = "Stocked full of tech toys, massage chairs, Xamarin engineers, and a few really cool surprises, the Darwin Lounge is your place to hang out and relax between sessions. Back by popular demand, we’ll have several code challenges—Mini-Hacks—for you to complete to earn awesome prizes.",
                StartTime = new DateTime (2016, 4, 28, 12, 0, 0, DateTimeKind.Utc),
                EndTime = new DateTime (2016, 4, 28, 20, 0, 0, DateTimeKind.Utc),
                LocationName = "Darwin Lounge",
                IsAllDay = false,

            });

            Events.Add(new FeaturedEvent
                {
                    Title = "General Session",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 28, 13, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 28, 14, 30, 0, DateTimeKind.Utc),
                    LocationName ="General Session",
                    IsAllDay = false,
                });


            Events.Add(new FeaturedEvent
                {
                    Title = "Closing Session & Xammy Awards",
                    Description = "",
                    StartTime = new DateTime(2016, 4, 28, 20, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2016, 4, 28, 21, 30, 0, DateTimeKind.Utc),
                    LocationName="General Session",
                    IsAllDay = false,

                });
        }

        public override async Task<IEnumerable<FeaturedEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject (Events);
            return Events;
        }
    }
}

