using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityFeedSuccess")]
    public class ActivityFeedSuccess : Activity
    {
        Button btncontinue_feed;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.feedback_success);

            // Create your application here

            btncontinue_feed = FindViewById<Button>(Resource.Id.btncontinue_feed);

            btncontinue_feed.Click += Btncontinue_feed_Click;
        }

        private void Btncontinue_feed_Click(object sender, EventArgs e)
        {
            Intent backsettings = new Intent(this, typeof(SettingsActivity));
            StartActivity(backsettings);
        }
    }
}