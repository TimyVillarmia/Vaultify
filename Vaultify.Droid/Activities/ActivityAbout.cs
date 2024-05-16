using System;
using System.Linq;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using AndroidX.Fragment.App;
using Firebase.Auth;
using Firebase.Firestore.Auth;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Vaultify.Droid.Common;
using Vaultify.Droid.Fragments;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityAbout")]
    public class ActivityAbout : Activity
    {
        ImageView policy_back;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.About);

            // Create your application here

            policy_back = FindViewById<ImageView>(Resource.Id.policy_back);

            policy_back.Click += policy_back_Click;
        }

        private void policy_back_Click(object sender, EventArgs e)
        {
            Intent backsettings = new Intent(this, typeof(SettingsActivity));
            StartActivity(backsettings);
        }
    }
}