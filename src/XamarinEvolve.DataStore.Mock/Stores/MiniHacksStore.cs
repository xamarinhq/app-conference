using System;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XamarinEvolve.DataStore.Mock
{
    public class MiniHacksStore : BaseStore<MiniHack>, IMiniHacksStore
    {
        List<MiniHack> hacks;
        public MiniHacksStore()
        {
        }

        public override async Task<IEnumerable<MiniHack>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore();
            return hacks as IEnumerable<MiniHack>;
        }

        public override Task InitializeStore()
        {
            if (hacks != null)
                return Task.FromResult(true);

            hacks = new List<MiniHack>();
            hacks.Add(new MiniHack
                {
                    UnlockCode="37786",
                    BadgeUrl="https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164903/wear-hack.png",
                    Name="Android Wear",
                    Subtitle = "Featuring Intel powered Fossil Q watch.",
                    Description = "Build your very own custom watch face for Android Wear to display time and your current step count.",
                    GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
                });
            hacks.Add(new MiniHack
                {
                    UnlockCode="9734",
                    BadgeUrl="https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164903/bitrise-hack.png",
                    Name = "Bitrise",
                    Subtitle = "Continuous Integration & Deployment to the Max!",
                    Description = "End-to-end continous integration and deployment. Automatically build your project with every commit and link into the deep integration with Xamarin Test Cloud and Xamarin Insights.",
                    GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
                });
            hacks.Add(new MiniHack
                {
                    BadgeUrl="https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164902/hue-hack.png",
                    Name = "Philips Hue",
                    Subtitle = "Make those light glow!",
                    Description = "Control your home and make Xamarin Evolve glow with HomeKit and Philips Hue.",
                    GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
                });
            hacks.Add(new MiniHack
                {
                    UnlockCode="32015",
                    BadgeUrl="https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164902/twilio-hack.png",
                    Name = "Twilio",
                    Subtitle = "Ring ring, who's there?",
                    Description = "Build your very first real time communication moabile apps powered by Twilio's latest messaging, voice, and video APIs. !",
                    GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
                });
            hacks.Add(new MiniHack
                {
                    UnlockCode="94942",
                    BadgeUrl = "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164902/azure-hack.png",
                    Name = "Azure Mobile Apps",
                    Subtitle = "Online? Offline? Don't worry your data is available and in sync.",
                    Description = "Build your very own online and offline sycnrhonized mobile application across all mobile platforms. Build a full .NET backend or get up and running fast with Easy Tables.",
                    GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
                });

            hacks.Add (new MiniHack {
                UnlockCode="78476",
                BadgeUrl="https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164902/forms-hack.png",
                Name = "Xamarin.Forms",
                Subtitle = "All the platforms, One shared UI, 100% Native",
                Description = "Checkout the latest and greatest features of Xamarin.Forms. Build fully native iOS, Android, and Windows apps completely from shared code and XAML.",
                GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
            });

            hacks.Add (new MiniHack {
                UnlockCode="77213",
                BadgeUrl="https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/19164903/hockeyapp-hack.png",
                Name = "HockeyApp",
                Subtitle = "Deliver your app to your testers faster!",
                Description = "Distribute to iOS, Android, Windows Get Crash Reports and Feedback! Setup your first app in HockeyApp and see all of the additional integrations in the HockeyApp SDK for your Xamarin apps.",
                GitHubUrl = "https://github.com/xamarin/mini-hacks/blob/master/Xamarin.Forms/README.md"
            });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject (hacks);

            return Task.FromResult(true);
        }
    }
}

