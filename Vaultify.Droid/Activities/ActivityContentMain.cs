using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Google.Android.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityContentMain")]
    public class ActivityContentMain : Activity
    {
        TextView textView;
        MaterialButton signout;
        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.content_main);
            // Create your application here
            auth = FirebaseRepository.getFirebaseAuth();
            textView = FindViewById<TextView>(Resource.Id.textView1);
            signout = FindViewById<MaterialButton>(Resource.Id.signout);

            signout.Click += Signout_Click;

        }

        private void Signout_Click(object sender, EventArgs e)
        {


        }
    }
}