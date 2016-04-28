using System;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinEvolve.UITests
{
        public class FeedPage : BasePage
        {
                string AnnoucementItem = "AnnouncementItem";
                string SocialItem = "TweetItem";
                string FavoriteItem = "SessionItem" ; //TODO: Create Favs after creating accounttree

                public FeedPage()
                    : base (x => x.Class("Toolbar").Descendant().Text("Evolve Feed"), x => x.Class("UINavigationBar").Id("Evolve Feed"))
                {
                        if (OnAndroid)
                            {
                            }

                        if (OniOS)
                            {
                            }
                    }

                //TODO: maybe select specific session
                public void SelectAnnouncement()
                {
                        app.WaitForElement(AnnoucementItem);
                        app.Screenshot("Selecting First Upcoming Session");
                        app.Tap(AnnoucementItem);
                    }

                public void SelectSocialItem()
                {
                        app.WaitForElement(SocialItem);
                        app.Screenshot("Tapping on First Social Item");
                        app.Tap(SocialItem);
                    }
                    
                //TODO: Once we have account setup, Test Favorites List
                public void SelectFavorite()
                {
                        app.WaitForElement(FavoriteItem);
                        app.Screenshot("Tapping on First Favorite Item");
                        app.Tap(FavoriteItem);
                    }
                   
            }
}