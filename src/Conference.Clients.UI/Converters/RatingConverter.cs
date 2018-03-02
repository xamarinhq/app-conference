using System;
using Xamarin.Forms;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;
using System.Globalization;

namespace Conference.Clients.UI
{
    /// <summary>
    /// Rating converter for display text
    /// </summary>
    class RatingConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rating = (int)value;
            if(rating == 0)
                return "Choose a rating";
            if (rating == 1)
                return "Not a fan";
            if (rating == 2)
                return "It was ok";
            if (rating == 3)
                return "Good";
            if (rating == 4)
                return "Great";
            if (rating == 5)
                return "Love it!";

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Determins if the rating section should be visible
    /// </summary>
    class RatingVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            #if DEBUG || ENABLE_TEST_CLOUD
            return true;
            #endif

            var session = value as Session;
            if (session == null)
                return false;

            if (!session.StartTime.HasValue)
                return false;

            //if it has started or is about to start
            if (session.StartTime.Value.AddMinutes(-15) < DateTime.UtcNow)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
