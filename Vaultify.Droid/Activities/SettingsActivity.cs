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
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        ImageView settingsback;
        Button button_delete;
        Button button_feedback;
        Button button_terms;
        Button button_policy;
        Button button_about;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingspage);

            // Create your application here

            settingsback = FindViewById<ImageView>(Resource.Id.settingsback);
            button_delete = FindViewById<Button>(Resource.Id.button_delete);
            button_feedback = FindViewById<Button>(Resource.Id.button_feedback);
            button_terms = FindViewById<Button>(Resource.Id.button_terms);
            button_policy = FindViewById<Button>(Resource.Id.button_policy);
            button_about = FindViewById<Button>(Resource.Id.button_about);

            settingsback.Click += settingsback_Click;
            button_delete.Click += button_delete_Click;
            button_feedback.Click += button_feedback_Click;
            button_terms.Click += button_terms_Click;
            button_policy.Click += button_policy_Click;
            button_about.Click += button_about_Click;
        }

        private void settingsback_Click(object sender, EventArgs e)
        {
            Intent goback = new Intent(this, typeof(ActivityHome));
            StartActivity(goback);
        }
        private void button_delete_Click(object sender, EventArgs e)
        {
            Intent delete = new Intent(this, typeof(ActivityDelSuccess));
            StartActivity(delete);
        }
        private void button_feedback_Click(object sender, EventArgs e)
        {
            Intent feedack = new Intent(this, typeof(ActivitySentFeed));
            StartActivity(feedack);
        }
        private void button_terms_Click(object sender, EventArgs e)
        {
            Intent terms = new Intent(this, typeof(ActivityTerms));
            StartActivity(terms);
        }
        private void button_policy_Click(object sender, EventArgs e)
        {
            Intent policy = new Intent(this, typeof(ActivityPolicy));
            StartActivity(policy);
        }
        private void button_about_Click(object sender, EventArgs e)
        {
            Intent about = new Intent(this, typeof(ActivityAbout));
            StartActivity(about);
        }
    }
}