using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using XamarinEvolve.Droid;

[assembly: ResolutionGroupName ("Xamarin")]
[assembly: ExportEffect (typeof (ListViewSelectionOnTopEffect), "ListViewSelectionOnTopEffect")]
namespace XamarinEvolve.Droid
{
    public class ListViewSelectionOnTopEffect : PlatformEffect
    {
        protected override void OnAttached ()
        {
            try 
            {
                var listView = Control as AbsListView;

                if (listView == null)
                    return;

                listView.SetDrawSelectorOnTop (true);
            } catch (Exception ex) {
                
            }
        }

        protected override void OnDetached ()
        {
            
        }
    }
}

