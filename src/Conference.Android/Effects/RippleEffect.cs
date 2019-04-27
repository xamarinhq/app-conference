using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Conference.Droid;
using Android.Util;

[assembly: ExportEffect(typeof(RippleEffect), "RippleEffect")]
namespace Conference.Droid
{
    public class RippleEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Container is Android.Views.View view)
                {
                    view.Clickable = true;
                    view.Focusable = true;

                    using (var outValue = new TypedValue())
                    {
                        view.Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, outValue, true);
                        view.SetBackgroundResource(outValue.ResourceId);
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDetached()
        {

        }
    }
}

