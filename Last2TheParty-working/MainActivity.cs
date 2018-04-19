using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Media;
using System.Threading.Tasks;
using System;
using Android.Views;
using Android.Support.V7.Widget;
using MainToolbar = Android.Widget.Toolbar;
using Android.Content;
using static Android.Content.ClipData;
using Android.Support.V7.App;

namespace Last2TheParty
{
    [Activity(Label = "Last2TheParty", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true)]
    public class MainActivity : Activity
    {
        List<Episode> episodes;
        L2TPService playerService;
        EpisodeRowAdapter episodeRowAdapter;
        RecyclerView.LayoutManager layoutManager;
        Intent episodeScreen;
        public static Episode chosenEpisode;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Load_Rss();

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.Main);
            ImageView image = FindViewById<ImageView>(Resource.Id.imgLogo);
            image.SetImageResource(Resource.Drawable.l2tp);

            var toolbar = FindViewById<MainToolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            toolbar.OverflowIcon = Resources.GetDrawable(Resource.Mipmap.ic_more_vert_black_24dp);
            ActionBar.Title = null;
            displayRss();
        }

        private void PlayAudio(string episodeURL)
        {
            try
            {
                if (playerService != null)
                {
                    playerService.Player.Stop();
                    playerService.Player.Reset();
                }
                else
                {
                    playerService = new L2TPService();
                }
                    
                playerService.Player.SetDataSource(episodeURL);
                playerService.Player.Prepare();
                playerService.Player.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void PauseAudio()
        {
            try
            {
                if (playerService != null)
                {
                    playerService.Player.Pause();
                }
          
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void ResumeAudio()
        {
            try
            {
                if (playerService != null)
                {
                    playerService.Player.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void displayRss()
        {
            //add episodes to list view
            RecyclerView rvEpisodes = FindViewById<RecyclerView>(Resource.Id.rvEpisodes);
            layoutManager = new LinearLayoutManager(this);
            rvEpisodes.SetLayoutManager(layoutManager);
            episodeRowAdapter = new EpisodeRowAdapter(episodes);
            episodeRowAdapter.ItemClick += OnEpisodeClick;
            rvEpisodes.SetAdapter(episodeRowAdapter);
            rvEpisodes.ScrollbarFadingEnabled = false;
        }

        private void OnEpisodeClick(object sender, int position)
        {
            //play or pause
            if (episodeRowAdapter.SelectedPosition == position && episodeRowAdapter.isPaused)
            {
                ResumeAudio();

                episodeScreen = new Intent(this, typeof(SingleEpisode));
                StartActivity(episodeScreen);
            }
            else if(episodeRowAdapter.SelectedPosition == position && !episodeRowAdapter.isPaused)
            {
                PauseAudio();
            }
            else
            {
                //play episode
                chosenEpisode = episodes[position];
                PlayAudio(chosenEpisode.media_url);

                episodeScreen = new Intent(this, typeof(SingleEpisode));
                StartActivity(episodeScreen);
            }

            
          
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

        public void Load_Rss()
        {
            RssParse rssParse = new RssParse();

            string valid = Task.Run(async ()=> { return await rssParse.ValidateDownload(); }).Result;

            if(valid.IndexOf("true") == -1)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Invalid Application");
                alert.SetMessage("Please download a valid application from the Play store.");
                alert.SetButton("OK", (c, ev) =>
                {
                    this.CloseContextMenu();
                    this.FinishAffinity();
                    
                });
                alert.Show();
            }

            try
            {
                episodes = Task.Run(async () => { return await rssParse.GetEpisodes(); }).Result;
            }
            catch (Exception ex)
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Failed to Load Epsiodes");
                alert.SetMessage("The RSS feed failed to load the episodes. Error: " + ex.ToString());
                alert.SetButton("OK", (c, ev) =>
                {
                    this.CloseContextMenu();
                });
                alert.Show();
            }

        }
    }
}

