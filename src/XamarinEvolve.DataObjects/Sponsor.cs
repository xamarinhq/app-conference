using System;

namespace XamarinEvolve.DataObjects
{
    public class Sponsor : BaseDataObject
    {
        /// <summary>
        /// Gets or sets the name of sponsor
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sponsor level.
        /// </summary>
        /// <value>The sponsor level.</value>
        public virtual SponsorLevel SponsorLevel { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Transparent PNG Rectangle
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the website URL: http://www.xamarin.com
        /// </summary>
        /// <value>The website URL.</value>
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the twitter profile: 
        /// For http://twitter.com/JamesMontemagno this is: JamesMontemagno NO @
        /// </summary>
        /// <value>The twitter URL.</value>
        public string TwitterUrl { get; set; }

        /// <summary>
        /// Gets or sets the booth location.
        /// </summary>
        /// <value>The booth location.</value>
        public string BoothLocation { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// 0 means put it at the top of the SponsorLevel
        /// </summary>
        /// <value>The rank.</value>
        public int Rank { get; set; }
        #if MOBILE
        [Newtonsoft.Json.JsonIgnore]
        public Uri ImageUri 
        { 
            get 
            { 
                try
                {
                    return new Uri(ImageUrl);
                }
                catch
                {

                }
                return null;
            } 
        }
        #endif
    }
}