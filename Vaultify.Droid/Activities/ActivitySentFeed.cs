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
    [Activity(Label = "ActivitySentFeed")]
    public class ActivitySentFeed : Activity
    {
        ImageView feedgoback;
        Button btnsendfeed;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.sendfeedback);

            // Create your application here

            feedgoback = FindViewById<ImageView>(Resource.Id.feedgoback);
            btnsendfeed = FindViewById<Button>(Resource.Id.btnsendfeed);

            feedgoback.Click += feedgoback_Click;
            btnsendfeed.Click += btnsendfeed_Click;
        }

        private void feedgoback_Click(object sender, EventArgs e)
        {
            Intent goback = new Intent(this, typeof(SettingsActivity));
            StartActivity(goback);
        }

        private void btnsendfeed_Click(object sender, EventArgs e)
        {
            Intent send = new Intent(this, typeof(ActivityFeedSuccess));
            StartActivity(send);
        }
    }
}