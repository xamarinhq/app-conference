using Conference.DataStore.Abstractions;
using Conference.DataObjects;
using Conference.DataStore.Mock;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Conference.DataStore.Mock
{
    public class SponsorStore : BaseStore<Sponsor>, ISponsorStore
    {
        List<Sponsor> Sponsors;
        readonly static string [] Companies =
            {
                "Airwatch",
                "AppDome",
                // "Apperian",
                "Arxan",
                "Avanade",
                "Bitrise",
                "BlueTube",
                "Citrix XenMobile",
                "Cloudmine",
                "ComponentOne",
                "Couchbase",
                "Dropbox",
                "Esri",
                "Google",
                "Intel",
                "Judo Payments",
                "Kinvey",
                "Microsoft",
                "Mindtree",
                "Neudesic",
                "Nventive",
                "Pariveda",
                // "PayPal",
                "Philips Hue",
                "Scandit",
                "Sogeti",
                "Syncfusion",
                "Twilio",
                "Urban Airship",
                "WillowTree",
                "Zebra Technologies",
                "Zumero"
            };

        readonly static int [] Levels =
            {
                2,
                3,
            // 3,
            2,
            1,
            3,
            2,
            3,
            3,
            3,
            2,
            2,
            2,
            1,
            2,
            2,
            3,
            0,
            3,
            3,
            3,
            2,
            // 3,
            2,
            3,
            2,
            3,
            2,
            3,
            3,
            2,
            3
            };

        readonly static string [] Logos =
            {
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193046/Airwatch.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193047/AppDome.png",
                // "Apperian",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193047/Arxan.png", // Arxan
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193047/Avanade.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193047/Bitrise.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193048/Bluetube.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193048/Citrix.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193048/Cloudmine.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193051/Xuni.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193048/Couchbase.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193048/Dropbox.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193048/Esri.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193049/Google.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193049/Intel.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193049/Judo.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193049/Kinvey.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193049/Microsoft.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193050/Mindtree.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193050/Neudesic.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193050/nventive.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193050/Pariveda.png",
                // "PayPal",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193050/Philips.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193050/Scandit.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193051/Sogeti.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193051/Syncfusion.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193051/Twilio.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193051/Urban-Airship.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193051/Willowtree.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193052/Zebra.png",
                "https://s3.amazonaws.com/blog.xamarin.com/wp-content/uploads/2016/03/16193052/Zumero.png"
            };

        readonly static string [] Descriptions =
        {
                "AirWatch by VMware is the leader in enterprise mobility management, with more than 16,000 global customers. The AirWatch platform includes industry-leading mobile device, email, application, content and browser management solutions. Organizations can implement these solutions across device types and use cases, including complete EMM for corporate and line of business deployments, and containerized solutions for bring your own device (BYOD) programs.",
                "AppDome provides an essential shield that secures mobile apps from advanced cyber threats, mobile fraud, IP rights infringement and data theft.  AppDome's core App Fusion technology offers the ability to fuse new features and new capabilities into existing consumer and enterprise apps on iOS and Android by uploading the final package to the AppDome Service.",
                // "Apperian",
                "Arxan",
                "Avanade leads in providing innovative digital services, business solutions and design-led experiences for its clients, delivered through the power of people and the Microsoft ecosystem. Majority owned by Accenture, Avanade was founded in 2000 by Accenture LLP and Microsoft Corporation and has 27,000 professionals in 23 countries. Visit us at avanade.com.",
                "Mobile Continuous Integration and Delivery for your whole team, with dozens of integrations for your favourite services. Chain these integrations together to create workflows for different branches with a unique, visual workflow editor. Use Xamarin Test Cloud and Xamarin Insights seamlessly through a CI environment and start automating with confidence.",
                "Bluetube builds tomorrow’s enterprise mobile experiences for today’s forward-thinking organizations. With an agile approach, we guide clients to mobile solutions by providing user experience and technology consulting. Our mid-market, non profit and global 2000 clients span industries including healthcare, manufacturing, consumer products, arts and entertainment.",
                "XenMobile is the most comprehensive Enterprise Mobility Management solution delivering mobile device, mobile app and mobile content management along with business-class productivity apps (including secure email), that enhance the user experience without compromising security.  Users get email, calendar and contact apps with Outlook-like productivity.  XenMobile also provides document editing, note-taking, and remote desktop access. XenMobile’s unified app store allows users to run any app, even non-mobile apps. Plus, users can access, sync, and edit files from anywhere.  ",
                "CloudMine's Connected Health SDK empowers organizations to deliver secure, HIPAA-compliant mobile apps using the Xamarin SDK. Our backend platform enables end-to-end C# development, data storage, embedded search, & much more - taking apps from test to deployment - instantly.",
                "Xuni is a collection of native, cross-platform mobile UI controls. Save valuable development time with high-quality, feature-packed grids, charts, and gauges that add animated visualizations to your applications. Deliver the same experience across iOS, Android and the Xamarin platform using native programming techniques. Download trial goxuni.com.",
                "Couchbase",
                "Dropbox",
                "Esri helps organizations map and model our world. Esri’s GIS technology enables them to effectively analyze and manage their geographic information and make better decisions. ArcGIS is a powerful platform for developers to create geospatial applications for mobile, web and desktops. Developers can access Esri’s cloud services, mapping APIs and SDKs, ready-to-use content, and self-hosted solutions. Learn more at go.esri.com/Conference.",
                "Google Developers provides tools and programs that make it easier for individuals and organizations to develop great apps, grow and engage with users, and earn money.",
                "Though best recognized as the world's leading supplier of semiconductors, Intel offers the software community a wide range of software tools, community resources and technical support for efficient development of innovative Android apps for mobile and wearable devices powered by Intel® Architecture. The Intel® Software Developer Zone allows developers to engage with Intel and other devs for all things software. Get development tools, technical articles, and support by visiting the Intel® Developer Zone for Android today.",
                "Judo Payments are leader in AppCommerce. Judo provides secure in-app payments to leading companies globally. Our payments experts help guide businesses and their development partners on how to create best in class apps to make paying faster, easier and more secure. Judo’s native app SDK are built for both core platform as well as cross-platform such as Xamarin in order to provide consistent app experience regardless of the operating system. Get started here to integrate payments on Xamarin components.xamarin.com/view/judopay-xamarin-sdk",
                "Kinvey, the leading enterprise Mobile Backend as a Service provider, allows Xamarin developers to get started instantly and have the power of the most complete mBaaS at their fingertips.  With Kinvey you can quickly deliver any B2B, B2C, or B2E app for any use case, on any device.  Try it for free console.kinvey.com/sign-up",
                "Microsoft is the leading platform and productivity company for the mobile-first, cloud-first world, and its mission is to empower every person and every organization on the planet to achieve more. The Visual Studio team works on tools for every developer and every app.",
                "Mindtree delivers digital transformation and technology services from ideation to execution, enabling Global 2000 clients to outperform the competition. Mindtree takes an agile, collaborative approach to creating customized solutions across the digital value chain. Our deep expertise in infrastructure and applications management helps optimize your IT into a strategic asset. ",
                "Neudesic delivers technology-driven solutions for today’s business challenges. As a Xamarin Elite Consulting Partner, we blend our mobility expertise with their tools and technology to deliver the best mobile solutions for companies nationwide. From ideation to delivery to management, Neudesic is there every step of the way. Visit neudesic.com.",
                "nventive originates from the mind and will of Agile development coaches dedicated to offering the best mobile application development experience. Our commitment to our clients and our agile mindset translate into flexibility, transparency and collaboration. Our goal is that our clients consider us as an extension of their team.  ",
                "Pariveda",
                // "PayPal",
                "Philips Lighting, a Royal Philips (NYSE: PHG, AEX: PHIA) company, is the global leader in lighting products, systems and services. Our understanding of how lighting positively affects people coupled with our deep technological know-how enable us to deliver digital lighting innovations that unlock new business value, deliver rich user experiences and help to improve lives. We lead the industry in connected lighting systems and services, leveraging the Internet of Things to take light beyond illumination.",
                "Scandit delivers high performance mobile barcode scanning solutions for smartphones, tablets and wearables. Scandit's lightning-fast and accurate Barcode Scanner is a valuable addition to any enterprise application. Our Xamarin component utilizes our unique blurry barcode scanning technology that works across platforms to scan any barcode type from any angle.",
                "Sogeti",
                "Founded in 2001, Syncfusion is the enterprise technology partner of choice for software development, delivering a broad range of web, mobile, and desktop controls. Its comprehensive suite of components includes charts, grids, maps, tree maps, editors, and file format manipulation libraries for Xamarin.iOS, Xamarin.Android, and Xamarin.Forms platforms.",
                "Twilio is a cloud communications platform for software developers to build, scale and operate real time communications in their software applications. Twilio powers the future of business communications, enabling phones, VoIP, and messaging to be embedded into web, desktop, and mobile software. We take care of the messy telecom hardware and expose a globally available cloud API that developers can interact with intelligent & complex communications systems",
                "We power how businesses connect and communicate with people by unlocking the full potential of mobile. Urban Airship helps leading brands engage their mobile users and build high-value relationships from the moment customers download an app. Thousands of companies and some of the most demanding brands in retail, media & entertainment, sports and travel & hospitality, trust Urban Airship to deliver the mobile moments that matter to their customers and to their business.",
                "WillowTree, Inc. is a leading mobile strategy, design and app development company that bridges the highest level of consumer user experience with enterprise-grade deployments and security.  Learn how WillowTree can help your business by visiting willowtreeapps.com.",
                "Zebra Technologies is a global leader in enterprise asset intelligence, designing and marketing specialty printers, mobile computing, data capture, radio frequency identification products and real-time locating systems. Incorporated in 1969, the company has over 7,000 employees worldwide and provides visibility into valued assets, transactions and people.",
                "Zumero is a replicate-and-sync solution for mobile apps and SQL Server.  Zumero enables Xamarin developers to create data-driven business apps for mobile workers who need to collaborate and share data in both online and offline scenarios.  Zumero also offers complete mobile app development services for Xamarin."
        };

        readonly static string [] Handles = {
                "AirWatch",
                "App_Dome",
                // "Apperian",
                "Arxan",
                "AvanadeInc",
                "Bitrise",
                "bluetubei",
                "XenMobile",
                "Cloudmine",
                "goxuni",
                "Couchbase",
                "Dropbox",
                "Esri",
                "googledevs",
                "intelsoftware",
                "judopayments",
                "Kinvey",
                "visualstudio",
                "Mindtree_Ltd",
                "Neudesic",
                "nventive",
                "Pariveda_Inc",
                // "PayPal",
                "tweethue",
                "Scandit",
                "Sogeti",
                "Syncfusion",
                "Twilio",
                "urbanairship",
                "willowtreeapps",
                "ZebraTechnologies",
                "zumero_uno"
        };

        readonly static string [] Websites = {
                "air-watch.com",
                "appdome.com",
                // "Apperian",
                "arxan.com",
                "avanade.com",
                "bitrise.io",
                "bluetubeinc.com",
                "citrix.com",
                "cloudmine.me",
                "componentone.com",
                "couchbase.com",
                "dropbox.com",
                "esri.com",
                "developers.google.com",
                "software.intel.com/en-us/android",
                "judopay.com",
                "kinvey.com",
                "visualstudio.com",
                "mindtree.com",
                "neudesic.com",
                "nventive.com",
                "parivedasolutions.com",
                // "PayPal",
                "meethue.com",
                "scandit.com",
                "sogeti.com",
                "syncfusion.com",
                "twilio.com",
                "urbanairship.com",
                "willowtreeapps.com",
                "zebra.com",
                "zumero.com"
        };


        public override async  Task<Sponsor> GetItemAsync(string id)
        {
            await InitializeStore();
            return Sponsors.FirstOrDefault(s => s.Id == id);
        }

        public override async Task<IEnumerable<Sponsor>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject (Sponsors);
            return Sponsors as IEnumerable<Sponsor>;
        }

        bool initialized;
        public override Task InitializeStore()
        {
            if (initialized)
                return Task.FromResult(true);

            initialized = true;

            Sponsors = new List<Sponsor>();
            for (var i = 0; i < Companies.Length; i++)
            {
                Sponsors.Add(new Sponsor
                    {
                        Name = Companies[i],
                        ImageUrl = Logos[i],
                        Description = Descriptions[i],
                        WebsiteUrl = Websites[i],
                        TwitterUrl = Handles[i],
                        SponsorLevel = GetLevel(Levels[i])
                    });
            }

            return Task.FromResult(true);

        }
        List<SponsorLevel> sponsorLevels;
        SponsorLevel GetLevel(int level)
        {
            if (sponsorLevels == null) {
                    sponsorLevels = new List<SponsorLevel> {
                    new SponsorLevel { Name = "Platinum", Rank = 0 },
                    new SponsorLevel { Name = "Gold", Rank = 1 },
                    new SponsorLevel{ Name = "Silver", Rank = 2 },
                    new SponsorLevel { Name = "Exhibitor", Rank = 3 }
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject (sponsorLevels);
            }



            return sponsorLevels[level];
        }
    }
}