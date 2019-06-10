using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
#if MOBILE
using System.Windows.Input;
#endif

namespace DataManager.Models
{
    public class Session : BaseEntity
    {
        public Session() {
            this.Speakers = new List<Speaker>();
            this.Categories = new List<Category>();
        }
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the short title that is displayed in the navigation bar
        /// For instance "Intro to X.Forms"
        /// </summary>
        /// <value>The short title.</value>
        /// 
        public string ShortTitle { get; set; }

        /// <summary>
        /// Gets or sets the abstract.
        /// </summary>
        /// <value>The abstract.</value>
        [UIHint("MultilineText")]
        public string Abstract { get; set; }

        /// <summary>
        /// Gets or sets the speakers.
        /// </summary>
        /// <value>The speakers.</value>
        [UIHint("Speakers")]
        public ICollection<Speaker> Speakers { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>The room.</value>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The main categories.</value>
        public ICollection<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        [UIHint("StartTime")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        [UIHint("EndTime")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }


        /// <summary>
        /// Gets or sets the level of the session [100 - 400]
        /// </summary>
        /// <value>The session level.</value>
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the url for the presentation material
        /// </summary>
        /// <value>The presentation material.</value>
        public string PresentationUrl { get; set; }

        /// <summary>
        /// Gets or sets the url for the recorded session video
        /// </summary>
        /// <value>The url for the recorded session video.</value>
        public string VideoUrl { get; set; }
    }
}