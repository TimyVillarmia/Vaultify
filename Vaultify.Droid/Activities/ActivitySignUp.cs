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
using Vaultify.Droid.Common.IViews;
using Vaultify.Droid.Presenters;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : AppCompatActivity, ISignUpVew
    {
        public event EventHandler SignUp;

        public void SignIn()
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            // Create your application here
            SetContentView(Resource.Layout.signup);

            // get the auth from the repository
            FirebaseRepository firebaseRepository = new FirebaseRepository();
            // start firebase


            // pass the firebaseRepository object
            // allows activities below get access all method of this
            new SignUpPresenter(this, firebaseRepository);

        }





    }
}