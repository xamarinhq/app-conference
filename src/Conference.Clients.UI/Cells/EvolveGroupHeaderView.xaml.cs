using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conference.Clients.UI
{

    public class ConferenceGroupHeader : ViewCell
    {
        public ConferenceGroupHeader()
        {
            View = new ConferenceGroupHeaderView();
        }
    }
    public partial class ConferenceGroupHeaderView : ContentView
    {
        public ConferenceGroupHeaderView()
        {
            InitializeComponent();
        }
    }
}

