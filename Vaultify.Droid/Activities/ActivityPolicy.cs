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
    [Activity(Label = "ActivityPolicy")]
    public class ActivityPolicy : Activity
    {
        ImageView policy_back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingspage);

            // Create your application here

            policy_back = FindViewById<ImageView>(Resource.Id.policy_back);

            policy_back.Click += Policy_back_Click1;
        }

        private void Policy_back_Click1(object sender, EventArgs e)
        {
            Intent backtosettings = new Intent(this, typeof(SettingsActivity));
            StartActivity(backtosettings);
        }
    }
}