using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
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
            TextFieldEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldEmail);
            btnSignIn = FindViewById<MaterialButton>(Resource.Id.btnSignin);




            var EditTextEmail = TextFieldEmail.EditText;

            CreateAccountLink.Click += Signup_Click;
            btnSignIn.Click += BtnSignIn_Click;


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



                Intent Home = new Intent(this, typeof(ActivityHome));
                StartActivity(Home);


            }

            Finish();

        }

        private void Signup_Click(object sender, EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);
        }
    }
}