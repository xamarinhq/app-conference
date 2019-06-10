using DataManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataManager.ViewModels
{
   
    public class SessionViewModel 
    {
        public List<Speaker> AvailableSpeakers { get; set; }
        public List<Category> AvailableCategories { get; set; }
        public List<Room> AvailableRooms { get; set; }
        public List<string> AvailableLevels { get; set; }
        public List<string> SubmittedCategories { get; set; }
        public List<string> SubmittedSpeakers { get; set; }
        public string SubmittedRoom { get; set; }
        public string submittedLevel { get; set; }
        public Session TrackedSession { get; set; }
    }
}