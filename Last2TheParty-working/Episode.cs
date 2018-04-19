using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Last2TheParty
{
    public class Episode
    {
        //matches json response
        public int item_id { get; set; }
        public int show_id { get; set; }
        public string release_date { get; set; }
        public string file_class { get; set; }
        public bool is_free { get; set; }
        public string item_title { get; set; }
        public string description { get; set; }
        public string item_subtitle { get; set; }
        public string thumbnail_url { get; set; }
        public string media_url { get; set; }
        public string media_url_libsyn { get; set; }
        public string permalink_url { get; set; }
        public string share_type { get; set; }
        public string duration { get; set; }
        public string ext { get; set; }
        public bool is_video { get; set; }
        public bool is_playing { get; set; }


    }
}