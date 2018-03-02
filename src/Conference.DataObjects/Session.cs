using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
#if MOBILE
using System.Windows.Input;
#endif

namespace Conference.DataObjects
{
    public class Session : BaseDataObject
    {
        public Session() {
            this.Speakers = new List<Speaker>();
        }
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
        public string ShortTitle { get; set; }

        /// <summary>
        /// Gets or sets the abstract.
        /// </summary>
        /// <value>The abstract.</value>
        public string Abstract { get; set; }

        /// <summary>
        /// Gets or sets the speakers.
        /// </summary>
        /// <value>The speakers.</value>
        public virtual ICollection<Speaker> Speakers { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>The room.</value>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets the main category.
        /// </summary>
        /// <value>The main category.</value>
        public virtual Category MainCategory { get; set; }

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

#if MOBILE
        private string speakerNames;
        [Newtonsoft.Json.JsonIgnore]
        public string SpeakerNames
        {
            get
            {
                if (speakerNames != null)
                    return speakerNames;

                speakerNames = string.Empty;
                
                if (Speakers == null || Speakers.Count == 0)
                    return speakerNames;

                var allSpeakers = Speakers.ToArray ();
                speakerNames = string.Empty;
                for (int i = 0; i < allSpeakers.Length; i++)
                {
                    speakerNames += allSpeakers [i].FullName;
                    if (i != Speakers.Count - 1)
                        speakerNames += ", ";
                }


                return speakerNames;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public DateTime StartTimeOrderBy { get { return StartTime.HasValue ? StartTime.Value : DateTime.MinValue; } }
        const string delimiter = "|";
        string haystack;
        [Newtonsoft.Json.JsonIgnore]
        public string Haystack
        {
            get
            {
                if (haystack != null)
                    return haystack;

                var builder = new StringBuilder();
                builder.Append(delimiter);
                builder.Append(Title);
                builder.Append(delimiter);
                if (!string.IsNullOrWhiteSpace(MainCategory?.Name))
                    builder.Append(MainCategory.Name);
                builder.Append(delimiter);
                if (Speakers != null)
                {
                    foreach (var p in Speakers)
                        builder.Append($"{p.FirstName} {p.LastName}{delimiter}{p.FirstName}{delimiter}{p.LastName}");
                }

                haystack = builder.ToString();
                return haystack;
            }
        }
        bool isFavorite;
        [Newtonsoft.Json.JsonIgnore]
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                SetProperty(ref isFavorite, value);
            }
        }

        bool feedbackLeft;
        [Newtonsoft.Json.JsonIgnore]
        public bool FeedbackLeft
        {
            get { return feedbackLeft; }
            set
            {
                SetProperty(ref feedbackLeft, value);
            }
        }

        #endif
    }
}