using System;
using Humanizer;
using System.Windows.Input;
using Xamarin.Forms;
using FormsToolkit;
using Newtonsoft.Json;

namespace Conference.Clients.Portable
{
    public class Tweet
    {
        public Tweet()
        {
        }
        string tweetedImage;
        string fullImage;
        [JsonIgnore]
        public bool HasImage
        {
            get { return !string.IsNullOrWhiteSpace(tweetedImage); }
        }
        [JsonProperty("tweetedImage")]
        public string TweetedImage
        {
            get => tweetedImage;
            set
            {
                tweetedImage = value;
                fullImage = value;
                if (!string.IsNullOrWhiteSpace(tweetedImage))
                {
                    tweetedImage += ":thumb";
                }
            }
        }

        ICommand  fullImageCommand;
        public ICommand FullImageCommand =>
            fullImageCommand ?? (fullImageCommand = new Command(ExecuteFullImageCommand)); 

        void ExecuteFullImageCommand()
        {
            if (string.IsNullOrWhiteSpace(fullImage))
                return;
            MessagingService.Current.SendMessage(MessageKeys.NavigateToImage, fullImage);
        }

        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("screenName")]
        public string ScreenName { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public string TitleDisplay { get { return Name; } }
        [JsonIgnore]
        public string SubtitleDisplay { get { return "@" + ScreenName; } }
        [JsonIgnore]
        public string DateDisplay { get { return CreatedDate.Humanize(); } }
        [JsonIgnore]
        public Uri TweetedImageUri 
        { 
            get 
            { 
                try
                {
                    if (string.IsNullOrWhiteSpace(TweetedImage))
                        return null;

                    return new Uri(TweetedImage);
                }
                catch
                {

                }
                return null;
            } 
        }
    }
}

