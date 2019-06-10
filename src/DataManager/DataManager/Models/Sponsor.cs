using System;
using System.ComponentModel.DataAnnotations;

namespace DataManager.Models
{
    public class Sponsor : BaseEntity
    {
        [Key]
        public string Id { get; set; }
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
        [UIHint("UiImage")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the website URL: http://www.xamarin.com
        /// </summary>
        /// <value>The website URL.</value>
        [Display(Name = "Website Url", Description = "E.g. http://www.microsoft.com")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the twitter profile: 
        /// For http://twitter.com/JamesMontemagno this is: JamesMontemagno NO @
        /// </summary>
        /// <value>The twitter URL.</value>
        [Display(Name = "Twitter profile name", Description= "Just the name of the user, no @ sign! E.g. TechDaysNL")]
        public string TwitterUrl { get; set; }

        /// <summary>
        /// Gets or sets the facebook profile name or ID: 
        /// For http://facebook.com/XpiritBV this is: XpiritBV
        /// </summary>
        /// <value>The facebook profile name.</value>
        [Display(Name = "Facebook profile name", Description = "Just the name or ID (number) of the user, which comes after http://facebook.com. E.g. XpiritBV or 1515519349")]
        public string FacebookProfileName { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn Url: 
        /// </summary>
        /// <value>The LinkedIn URL.</value>
        [Display(Name = "LinkedIn Url", Description = "Full url of the LinkedIn page of the sponsor. E.g. https://www.linkedin.com/company/9183026")]
        public string LinkedInUrl { get; set; }

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
        [Display(Name = "Rank / order", Description = "The rank or order in which this sponsor appears within the sponsor level. Sponsors are grouped into levels first, and then sorted by this value.")]
        public int Rank { get; set; }
    }
}