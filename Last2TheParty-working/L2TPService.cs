using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Last2TheParty
{
    [Service]
    public class L2TPService : Service
    {
        public MediaPlayer Player { get; set; }

        public L2TPService()
        {
            Player = new MediaPlayer();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
           
        }

        
        public override void OnDestroy()
        {
            if (Player != null) Player.Release();
        }
    }
}