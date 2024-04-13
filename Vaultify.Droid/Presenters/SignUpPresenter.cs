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
using Vaultify.Droid.Common.IViews;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Presenters
{
    public class SignUpPresenter
    {

        private readonly IFirebase _firebaseRepository;
        private readonly ISignUpVew _signUpActivity;
        public SignUpPresenter()
        {

        }

        public SignUpPresenter(ISignUpVew _view, IFirebase repository)
        {
            _firebaseRepository = repository;
            _signUpActivity = _view;


            _signUpActivity.SignUp += SignUpEvent;

        }

        private void SignUpEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}