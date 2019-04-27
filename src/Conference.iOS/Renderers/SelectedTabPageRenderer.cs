﻿using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Conference.iOS;
using UIKit;
using Conference.Clients.UI;
using Conference.Clients.Portable;

[assembly:ExportRenderer(typeof(TabbedPage), typeof(SelectedTabPageRenderer))]
namespace Conference.iOS
{
    public class SelectedTabPageRenderer : TabbedRenderer
    {
        public static void Initialize()
        {
            var test = DateTime.UtcNow;
        }

        public override void ViewWillAppear(bool animated)
        {
            
            if (TabBar?.Items == null)
                return;

            var tabs = Element as TabbedPage;
            if (tabs != null)
            {   
                for (var i = 0; i < TabBar.Items.Length; i++)
                {
                    UpdateItem(TabBar.Items[i],  tabs.Children[i].Icon);
                }
            }

            base.ViewWillAppear(animated);
        }

        void UpdateItem(UITabBarItem item, string icon)
        {
            if (item == null)
                return;
            try
            {
                icon = icon.Replace(".png", "_selected.png");
                if(item?.SelectedImage?.AccessibilityIdentifier == icon)
                    return;
                item.SelectedImage = UIImage.FromBundle(icon);
                item.SelectedImage.AccessibilityIdentifier = icon;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to set selected icon: " + ex);
            }

        }
    }
}

