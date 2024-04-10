﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Fragment;
using AndroidX.Fragment.App;
using AndroidX.Transitions;
using Firebase;
using Firebase.Firestore;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using Vaultify.Droid.Activities;
using Vaultify.Droid.Resources.layout;

namespace Vaultify.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        TextView signuptext;
        MaterialButton btnSignIn;
        TextInputLayout TextFieldEmail;

        FirebaseFirestore database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.signin);

            database = GetDatabase();



            signuptext = FindViewById<TextView>(Resource.Id.hyperlink_create);
            btnSignIn = FindViewById<MaterialButton>(Resource.Id.btnSignin);
            TextFieldEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldEmail);

            btnSignIn.Click += BtnSignIn_Click;
            signuptext.Click += Signup_Click;

        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            TextFieldEmail.HelperTextEnabled = true;

            if (string.IsNullOrEmpty(TextFieldEmail.EditText?.Text))
            {
                TextFieldEmail.HelperText = "Must not be empty";
            }
        }

        private FirebaseFirestore GetDatabase()
        {
            FirebaseFirestore database;

            var options = new FirebaseOptions.Builder()
                .SetProjectId("vaultify-1556e")
                .SetApplicationId("vaultify-1556e")
                .SetApiKey("AIzaSyDOF3-W3yS5DL24QY7fCv9s7VVHHW7BAIU")
                .SetStorageBucket("vaultify-1556e.appspot.com")
                .Build();

            var app = FirebaseApp.InitializeApp(this, options);
            database = FirebaseFirestore.GetInstance(app);

            return database;
        }

        private void Signup_Click(object sender, System.EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}