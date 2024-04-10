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
using static System.Net.Mime.MediaTypeNames;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignIn")]
    public class ActivitySignIn : Activity
    {
        TextView signup;
        TextInputLayout TextEmail;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signin);

            // Create your application here
            signup = FindViewById<TextView>(Resource.Id.hyperlink_create);
            TextEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldEmail);

            var EditTextEmail = TextEmail.EditText;

            signup.Click += Signup_Click;
        }



   

        private void Signup_Click(object sender, EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);
        }
    }
}