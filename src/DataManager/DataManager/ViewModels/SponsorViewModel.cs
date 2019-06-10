using DataManager.Models;
using System.Collections.Generic;

namespace DataManager.ViewModels
{
    public class SponsorViewModel 
    {
        public List<SponsorLevel> AvailableSponsorLevels { get; set; }
        public string SubmittedSponsorLevel { get; set; }
        public Sponsor TrackedSponsor { get; set; }
    }
}