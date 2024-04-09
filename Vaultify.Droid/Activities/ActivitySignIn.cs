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
    [Activity(Label = "ActivitySignIn")]
    public class ActivitySignIn : Activity
    {
        TextView signup;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            signup = FindViewById<TextView>(Resource.Id.hyperlink_create);
            signup.Click += Signup_Click;
        }

        private void Signup_Click(object sender, EventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Hello");
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            //Intent Signup = new Intent(this, typeof(ActivitySignUp));
            //StartActivity(Signup);
        }
    }
}