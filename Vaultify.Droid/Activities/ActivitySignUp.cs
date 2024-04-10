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
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : Activity
    {
        private readonly IFirebase _firebaseRepository;


        public ActivitySignUp(IFirebase repository)
        {
            _firebaseRepository = repository;

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.signup);

        }


    }
}