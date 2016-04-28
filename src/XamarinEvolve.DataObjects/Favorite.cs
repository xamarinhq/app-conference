namespace XamarinEvolve.DataObjects
{
    /// <summary>
    /// This is per user
    /// </summary>
    public class Favorite : BaseDataObject
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}