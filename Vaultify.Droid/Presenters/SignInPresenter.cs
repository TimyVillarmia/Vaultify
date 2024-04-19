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
using Vaultify.Droid.Common.IViews;

namespace Vaultify.Droid.Presenters
{
    public class SignInPresenter
    {
        private readonly IFirebase _firebaseRepository;
        private readonly ISignInView _signInActivity;
        public SignInPresenter()
        {

        }

        public SignInPresenter(ISignInView _view,IFirebase repository)
        {
            _firebaseRepository = repository;
            _signInActivity = _view;


            _signInActivity.SignIn += SignInEvent;

        }

        private void SignInEvent(object sender, EventArgs e)
        {
            // implement signin from firebase

            bool credentialsIsValid = true;
            if (credentialsIsValid)
            {
                _signInActivity.SignInSucces();
            }
        }
    }
}