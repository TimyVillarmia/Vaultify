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
    [Activity(Label = "ActivityDelSuccess")]
    public class ActivityDelSuccess : Activity
    {
        Button btncontinue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.delete_success);

            // Create your application here

            btncontinue = FindViewById<Button>(Resource.Id.btncontinue);

            btncontinue.Click += btncontinue_Click;
        }
        private void btncontinue_Click(object sender, EventArgs e)
        {
            Intent proceed = new Intent(this, typeof(SettingsActivity));
            StartActivity(proceed);
        }
    }
}