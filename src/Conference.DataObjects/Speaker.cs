using System.Collections.Generic;
﻿using System;

namespace Conference.DataObjects
{
    public class Speaker : BaseDataObject
    {
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


        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        /// <value>The biography.</value>
        public string Biography { get; set; }

        /// <summary>
        /// This is the big Hero Image (Rectangle)
        /// </summary>
        /// <value>The photo URL.</value>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// This is a small Square Image (150x150 is good)
        /// </summary>
        /// <value>The avatar URL.</value>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Name of position such as CEO, Head of Marketing
        /// </summary>
        /// <value>The name of the position.</value>
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
        public string TwitterUrl { get; set; }

        /// <summary>
        /// Gets or sets the linked in profile name.
        /// https://www.linkedin.com/in/jamesmontemagno we just need: jamesmontemagno
        /// </summary>
        /// <value>The linked in URL.</value>
        public string LinkedInUrl { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        #if MOBILE
        [Newtonsoft.Json.JsonIgnore]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        [Newtonsoft.Json.JsonIgnore]
        public string Title 
        {
            get
            {
                if (string.IsNullOrWhiteSpace (CompanyName))
                    return PositionName;
                
                return $"{PositionName}, {CompanyName}"; 
            } 
        }

        [Newtonsoft.Json.JsonIgnore]
        public Uri AvatarUri 
        { 
            get 
            { 
                try
                {
                    return new Uri(AvatarUrl);
                }
                catch
                {
                    
                }
                return null;
            } 
        }
        [Newtonsoft.Json.JsonIgnore]
        public Uri PhotoUri 
        { 
            get 
            { 
                try
                {
                    return new Uri(PhotoUrl);
                }
                catch
                {

                }
                return null;
            } 
        }
        #else
        public string Email { get; set; }
        #endif

    }
}