using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Last2TheParty
{
    public class EpisodeRowAdapter : RecyclerView.Adapter
    {
        public List<Episode> episodes;
        public event EventHandler<int> ItemClick;
        public EpisodeHolder Holder;
        public int SelectedPosition = -1;
        public bool isPaused = false;

        public EpisodeRowAdapter(List<Episode> episodes)
        {
            this.episodes = episodes;
        }

        public override int ItemCount { get { return episodes.Count; } }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Holder = holder as EpisodeHolder;

            // view.FindViewById<TextView>(Resource.Id.number).Text = (position-1).ToString();
            Holder.Title.Text = episodes[position].item_title;
            Holder.Duration.Text = episodes[position].duration;

            //display speaker or pause if playing
            if (SelectedPosition == position && isPaused)
            {
                Holder.PlayingImage.SetImageResource(Resource.Drawable.pause);
            }
            else if(SelectedPosition == position)
            {
                Holder.PlayingImage.SetImageResource(Resource.Drawable.speaker);
            }
            else
            {
                Holder.PlayingImage.SetImageResource(Resource.Drawable.empty);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.episodeLineItem, parent,false);

            EpisodeHolder eh = new EpisodeHolder(itemView, OnClick);

            return eh;
        }

        void OnClick( int position)
        {
            ItemClick?.Invoke(this, position);
            if(SelectedPosition == position || isPaused)
            {
                isPaused = !isPaused;
            }

            SelectedPosition = position;

            NotifyDataSetChanged();
        }
    }

    public class EpisodeHolder : RecyclerView.ViewHolder
    {
        public ImageView PlayingImage { get; set; }
        public TextView Title { get; set; }
        public TextView Duration { get; set; }

        public EpisodeHolder(View itemView, Action<int> listener) : base(itemView)
        {
            PlayingImage = itemView.FindViewById<ImageView>(Resource.Id.isPlaying);
            Title = itemView.FindViewById<TextView>(Resource.Id.title);
            Duration = itemView.FindViewById<TextView>(Resource.Id.duration);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}