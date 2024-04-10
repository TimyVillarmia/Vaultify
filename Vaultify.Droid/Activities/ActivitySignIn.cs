using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;
using static System.Net.Mime.MediaTypeNames;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignIn")]
    public class ActivitySignIn : Activity
    {
        private readonly IFirebase _firebaseRepository;


        TextView CreateAccountLink;
        TextInputLayout TextFieldEmail;
        MaterialButton btnSignIn;


        public ActivitySignIn(IFirebase repository)
        {
            _firebaseRepository = repository;

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signin);

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


            try
            {
                // insert Firebase Authentication method from the FirebaseRepo class
                // create payload for the User and Pass Credentials
            }
            catch (Exception ex)
            {
                    
            }
        }

        private void Signup_Click(object sender, EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);
        }
    }
}