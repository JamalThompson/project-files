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
using MainToolbar = Android.Widget.Toolbar;
using Android.Graphics;
using Square.Picasso;

namespace Last2TheParty
{
    [Activity(Label = "SingleEpisode", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SingleEpisode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            // Create your application here
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.singleEpisode);
            ImageView image = FindViewById<ImageView>(Resource.Id.imgEpisode);
            Picasso.With(this).Load(MainActivity.chosenEpisode.thumbnail_url).Into(image);
            var toolbar = FindViewById<MainToolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            toolbar.OverflowIcon = Resources.GetDrawable(Resource.Mipmap.ic_more_vert_black_24dp);
            ActionBar.Title = null;

            //description
            TextView description = FindViewById<TextView>(Resource.Id.description);
            description.Text = MainActivity.chosenEpisode.description;

            //bottom toolbar
            var editToolbar = FindViewById<Toolbar>(Resource.Id.episodeToolbar);
            editToolbar.Title = "Editing";
            editToolbar.InflateMenu(Resource.Menu.episodeMenu);
            editToolbar.MenuItemClick += (sender, e) => {
                Toast.MakeText(this, "Bottom toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}