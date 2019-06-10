using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class Speaker : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        [NotMapped]
        public string Name { get { return FirstName + " " + LastName; } }

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        /// <value>The biography.</value>
        [UIHint("MultilineText")]
        public string Biography { get; set; }

        /// <summary>
        /// This is the big Hero Image (Rectangle)
        /// </summary>
        /// <value>The photo URL.</value>
        [UIHint("UiImage")]
        [Display(Name = "Photo URL", Description = "The big 'hero' image, to be displayed on the speaker details. For best results, use a ~1024wx768h landscape picture.")]
        public string PhotoUrl { get; set; }
        /// <summary>
        /// This is a small Square Image (150x150 is good)
        /// </summary>
        /// <value>The avatar URL.</value>
        [UIHint("UiImage")]
        [Display(Name = "Avatar URL", Description = "A small square photo, 150x150 is good.")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Name of position such as CEO, Head of Marketing
        /// </summary>
        /// <value>The name of the position.</value>
        [Display(Name = "Position or role", Description = "E.g. CEO, Head of Marketing, Developer")]
        public string PositionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the company website URL.
        /// </summary>
        /// <value>The company website URL.</value>
        public string CompanyWebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the blog URL.
        /// </summary>
        /// <value>The blog URL.</value>
        public string BlogUrl { get; set; }

        /// <summary>
        /// Gets or sets the twitter profile: 
        /// For http://twitter.com/JamesMontemagno this is: JamesMontemagno NO @
        /// </summary>
        /// <value>The twitter URL.</value>
        [Display(Name = "Twitter profile name", Description= "Just the name of the user, no @ sign! E.g. TechDaysNL")]
        public string TwitterUrl { get; set; }

        /// <summary>
        /// Gets or sets the linked in profile name.
        /// https://www.linkedin.com/in/jamesmontemagno we just need: jamesmontemagno
        /// </summary>
        /// <value>The linked in URL.</value>
        [Display(Name = "LinkedIn profile name", Description= "We just need the name that comes after 'https://www.linkedin.com/in/', not the full url! E.g. marcelv")]
        public string LinkedInUrl { get; set; }

        /// <summary>
        /// Gets or sets the facebook profile name or ID: 
        /// For http://facebook.com/james.montemagno this is: james.montemagno
        /// </summary>
        /// <value>The facebook profile name.</value>
        [Display(Name = "Facebook profile name", Description = "Just the name or ID (number) of the user, which comes after http://facebook.com. E.g. james.montemagno or 1515519349")]
        public string FacebookProfileName { get; set; }

        /// <summary>
        /// Indicate if this speaker must be featured/highlighted for promotional purposes
        /// </summary>
        [Display(Name = "Feature this speaker", Description = "Indicate if this speaker must be featured/highlighted for promotional purposes")]
        public bool IsFeatured { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return Id;
        }

    }
}