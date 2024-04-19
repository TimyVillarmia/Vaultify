using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using Google.Android.Material.Button;
using Google.Android.Material.Snackbar;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;
using Vaultify.Droid.Common.IViews;
using Vaultify.Droid.Presenters;
using static System.Net.Mime.MediaTypeNames;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignIn")]
    public class ActivitySignIn : AppCompatActivity, ISignInView
    {


        TextView CreateAccountLink;
        TextInputLayout TextFieldEmail;
        MaterialButton btnSignIn;
        TextView linkForgot;

        public event EventHandler SignIn;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signin);



            // get the auth from the repository
            FirebaseRepository firebaseRepository = new FirebaseRepository();
            // start firebase


            // pass the firebaseRepository object
            // allows activities below get access all method of this class
            new SignInPresenter(this, firebaseRepository);

            // Create your application here
            CreateAccountLink = FindViewById<TextView>(Resource.Id.hyperlink_create);
            linkForgot = FindViewById<TextView>(Resource.Id.linkForgot);
            TextFieldEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldEmail);
            btnSignIn = FindViewById<MaterialButton>(Resource.Id.btnSignin);




            var EditTextEmail = TextFieldEmail.EditText;

            CreateAccountLink.Click += Signup_Click;
            btnSignIn.Click += BtnSignIn_Click;
            linkForgot.Click += LinkForgot_Click;

        }

        [Obsolete]
        public override void OnBackPressed()
        {
            Toast.MakeText(ApplicationContext, "You're about to exit the app", ToastLength.Long).Show();
        }




        private void LinkForgot_Click(object sender, EventArgs e)
        {
            Intent accountRecovery = new Intent(this, typeof(ActivityRecovery));
            StartActivity(accountRecovery);
        }


        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            TextFieldEmail.HelperTextEnabled = true;

            // base cases for input validations
            if (string.IsNullOrEmpty(TextFieldEmail.EditText?.Text))
            {
                TextFieldEmail.HelperText = "Must not be empty";
            }
            else
            {
                // invoke the event from the SignInPresenter
                SignIn?.Invoke(this, new EventArgs());

                View view = (View)sender;
                Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();

                

            }


        }

        private void Signup_Click(object sender, EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);

        }

        public void SignInSucces()
        {
            Intent Home = new Intent(this, typeof(ActivityHome));
            StartActivity(Home);
            Finish();
            //test
        }
    }
}