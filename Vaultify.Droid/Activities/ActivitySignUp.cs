using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : AppCompatActivity
    {
        TextView linkSignin;
        public void SignIn()
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public override void OnBackPressed()
        {
            Toast.MakeText(ApplicationContext, "You're about to exit the app", ToastLength.Long).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.signup);
            linkSignin = FindViewById<TextView>(Resource.Id.hyperlink_create);


            // get the auth from the repository
            FirebaseRepository firebaseRepository = new FirebaseRepository();
            // start firebase




            linkSignin.Click += LinkSignin_Click;

        }

        private void LinkSignin_Click(object sender, EventArgs e)
        {
            Intent signIn = new Intent(this, typeof(ActivitySignIn));
            StartActivity(signIn);
            Finish();
        }
    }
}