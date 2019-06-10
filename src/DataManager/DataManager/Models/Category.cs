using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DataManager.Models
{
    public class Category :BaseEntity
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name that is displayed during filtering
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets the short name/code that is displayed on the sessions page.
        /// For instance the short name for Xamarin.Forms is X.Forms
        /// </summary>
        /// <value>The short name.</value>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the color in Hex form, for instance #FFFFFF
        /// </summary>
        /// <value>The color.</value>
        public string Color { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}