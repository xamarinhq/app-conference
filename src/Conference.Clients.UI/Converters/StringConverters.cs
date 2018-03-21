using System;
using Xamarin.Forms;
using System.Globalization;

namespace Conference.Clients.UI
{
    /// <summary>
    /// Is favorite text converter.
    /// </summary>
    class IsFavoriteTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            
            return (bool)value ? "Unfavorite" : "Favorite";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Is favorite detail text converter.
    /// </summary>
    class IsFavoriteDetailTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            
            return (bool)value ? "Remove from Favorites" : "Add to Favorites";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Has reminder event text converter.
    /// </summary>
    class HasReminderEventTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            

            return (bool)value ? "Remove from Calendar" : "Add to Calendar";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Has reminder text converter.
    /// </summary>
    class HasReminderTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            
            return (bool)value ? "Remove from Calendar" : "Add to Calendar";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
       
}

