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
    [Activity(Label = "ActivityTerms")]
    public class ActivityTerms : Activity
    {
        ImageView terms_back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.terms);

            // Create your application here

            terms_back = FindViewById<ImageView>(Resource.Id.terms_back);

            terms_back.Click += Terms_back_Click;
        }

        private void Terms_back_Click(object sender, EventArgs e)
        {
            Intent tosettings = new Intent(this, typeof(SettingsActivity));
            StartActivity(tosettings);
        }
    }
}