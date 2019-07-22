using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Conference.DataObjects
{
    public class FeaturedEvent : BaseDataObject
    {
        /// <summary>
        /// Gets or sets the title of the event such as: Conference Keynote
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the descriptionof the event
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public DateTime? EndTime { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is all day.
        /// Such as Darwin Lounge for instance
        /// </summary>
        /// <value><c>true</c> if this instance is all day; otherwise, <c>false</c>.</value>
        public bool IsAllDay { get; set; }

        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the sponsors if there is one for the event
        /// </summary>
        /// <value>The sponsor.</value>
        public virtual ICollection<Sponsor> Sponsors { get; set; }

        /// <summary>
        /// Gets or sets the sessions if there is one for the event
        /// </summary>
        /// <value>The sessions.</value>
        public virtual ICollection<Session> Sessions { get; set; }

#if MOBILE
        [JsonIgnore]
        public bool HasSponsor => Sponsors?.Count > 0;
        [JsonIgnore]
        public DateTime StartTimeOrderBy { get { return StartTime.HasValue ? StartTime.Value : DateTime.MinValue; } }
#endif
    }
}

