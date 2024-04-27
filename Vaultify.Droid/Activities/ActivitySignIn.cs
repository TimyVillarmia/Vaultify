using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Common;
using Android.Gms.Tasks;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using Firebase;
using Firebase.Auth;
using Google.Android.Material.Button;
using Google.Android.Material.Snackbar;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vaultify.Droid.Common;
using static System.Net.Mime.MediaTypeNames;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignIn")]
    public class ActivitySignIn : AppCompatActivity, IOnCompleteListener
    {


        TextView CreateAccountLink;
        TextInputLayout TextFieldEmail;
        TextInputLayout TextFieldPass;
        MaterialButton btnSignIn;
        TextView linkForgot;

        FirebaseAuth auth;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signin);



            // Create your application here
            CreateAccountLink = FindViewById<TextView>(Resource.Id.hyperlink_create);
            linkForgot = FindViewById<TextView>(Resource.Id.linkForgot);
            TextFieldEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldEmail);
            TextFieldPass = FindViewById<TextInputLayout>(Resource.Id.textFieldPass);
            btnSignIn = FindViewById<MaterialButton>(Resource.Id.btnSignin);


            auth = FirebaseRepository.getFirebaseAuth();
            

            CreateAccountLink.Click += Signup_Click;
            btnSignIn.Click += BtnSignIn_Click;
            linkForgot.Click += LinkForgot_Click;


            TextFieldEmail.EditText.TextChanged += delegate { ValidateField(TextFieldEmail); };
            TextFieldPass.EditText.TextChanged += delegate { ValidateField(TextFieldPass); };



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
        private void ValidateField(TextInputLayout field)
        {


            if (string.IsNullOrEmpty(field.EditText?.Text))
            {
                field.Error = "Must not be empty.";
                btnSignIn.Clickable = false;
                return;

            }

            if (field.Id == TextFieldEmail.Id)
            {
                bool isEmail = Regex.IsMatch(field.EditText?.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                {
                    TextFieldEmail.Error = "Must be a valid email address";
                    btnSignIn.Clickable = false;
                    return;
                }

            }


            //textFieldFullname.ErrorEnabled = false;
            TextFieldEmail.ErrorEnabled = false;
            btnSignIn.Clickable = true;

        }


        private void BtnSignIn_Click(object sender, EventArgs e)
        {


            string email = TextFieldEmail.EditText?.Text.ToString().Trim();
            string pass = TextFieldPass.EditText?.Text;

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(pass))
            {
                //textFieldFullname.Error = "Must not be empty.";
                TextFieldEmail.Error = "Must not be empty.";
                TextFieldPass.Error = "Must not be empty.";

                return;
            }

            auth.SignInWithEmailAndPassword(email, pass)
                .AddOnCompleteListener(this, this);



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
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Login was successful!", ToastLength.Short).Show();
                SignInSucces();
                Finish();
            }
            else
            {
                Toast.MakeText(this, task.Exception.Message, ToastLength.Short).Show();
            }

        }
    }
}