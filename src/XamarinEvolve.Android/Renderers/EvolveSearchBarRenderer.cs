using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamarinEvolve.Droid;
using Android.Widget;

[assembly:ExportRenderer(typeof(SearchBar), typeof(EvolveSearchBarRenderer))]
namespace XamarinEvolve.Droid
{
    public class EvolveSearchBarRenderer : SearchBarRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            UpdateSearchIcon();
            UpdateCursorColor();
            UpdateSearchPlate();

        }

        void UpdateSearchPlate ()
        {
            var searchId = Control.Resources.GetIdentifier ("android:id/search_plate", null, null);
            if (searchId == 0)
                return;


            var image = FindViewById<Android.Views.View>(searchId);
            if (image == null)
                return;

            image.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }


        void UpdateSearchIcon ()
        {
            try 
            {
                var searchId = Control.Resources.GetIdentifier ("android:id/search_mag_icon", null, null);
                if (searchId == 0)
                    return;


                var image = this.FindViewById<ImageView> (searchId);
                if (image == null)
                    return;

                image.SetImageResource(Resource.Drawable.icon_search);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
            }
        }

        void UpdateCursorColor ()
        {
            AutoCompleteTextView textView = null;
            try 
            {
                var searchId = Control.Resources.GetIdentifier ("android:id/search_src_text", null, null);
                if (searchId == 0)
                    return;


                textView = this.FindViewById<AutoCompleteTextView> (searchId);
                if (textView == null)
                    return;


                var theClass = Java.Lang.Class.FromType(typeof(TextView));
                var theField = theClass.GetDeclaredField("mCursorDrawableRes");
                theField.Accessible = true;
                theField.Set(textView, Resource.Drawable.cursor_white);
               
            } 
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
            }

            try
            {
                if (textView == null)
                    return;
                textView.SetBackgroundResource(Resource.Drawable.searchview_background);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
            }

        }
    }
}

