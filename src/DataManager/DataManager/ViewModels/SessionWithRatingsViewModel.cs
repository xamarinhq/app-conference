namespace DataManager.ViewModels
{
    public class SessionWithRatingsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal AverageRating { get; set; }
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
    }
}