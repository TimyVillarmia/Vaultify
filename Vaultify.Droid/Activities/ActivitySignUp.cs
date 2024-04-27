using Android.App;
using Android.Content;
using Android.Gms.Common.Server.Response;
using Android.OS;
using Android.Runtime;
using Android.Security;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vaultify.Droid.Common;
using static Android.Provider.Telephony.Mms;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : AppCompatActivity
    {
        TextView linkSignin;
        TextInputLayout textFieldFullname;
        TextInputLayout textFieldcreateEmail;
        TextInputLayout textFieldcreatePass;
        TextInputLayout textFieldconfirmPass;

        MaterialButton btnSignup;

        FirebaseAuth auth;
        public void SignIn()
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public override void OnBackPressed()
        {
            Toast.MakeText(ApplicationContext, "You're about to exit the app", ToastLength.Long).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.signup);
            linkSignin = FindViewById<TextView>(Resource.Id.hyperlink_create);
            textFieldFullname = FindViewById<TextInputLayout>(Resource.Id.textFieldFullname);
            textFieldcreateEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldcreateEmail);
            textFieldcreatePass = FindViewById<TextInputLayout>(Resource.Id.textFieldcreatePass);
            textFieldconfirmPass = FindViewById<TextInputLayout>(Resource.Id.textFieldconfirmPass);
            btnSignup = FindViewById<MaterialButton>(Resource.Id.btnSignup);

            auth = FirebaseRepository.getFirebaseAuth();
            // get the auth from the repository
            FirebaseRepository firebaseRepository = new FirebaseRepository();
            // start firebase




            linkSignin.Click += LinkSignin_Click;
            btnSignup.Click += BtnSignup_Click;

       
            textFieldFullname.EditText.TextChanged += delegate { ValidateField(textFieldFullname); };
            textFieldcreateEmail.EditText.TextChanged += delegate { ValidateField(textFieldcreateEmail); };
            textFieldcreatePass.EditText.TextChanged += delegate { ValidateField(textFieldcreatePass); };
            textFieldconfirmPass.EditText.TextChanged += delegate { ValidateField(textFieldconfirmPass); };
        }

        private void ValidateField(TextInputLayout field)
        {

            
            if (string.IsNullOrEmpty(field.EditText?.Text))
            {
                field.Error = "Must not be empty.";
                btnSignup.Clickable = false;
                return;

            }
            if (field.Id == textFieldFullname.Id)
            {
                if (field.EditText?.Text.Length < 4)
                {
                    textFieldFullname.Error = "Name too short. Please enter at least 4 characters.";
                    btnSignup.Clickable = false;
                    return;
                }
                else
                {
                    textFieldFullname.ErrorEnabled = false;
                    btnSignup.Clickable = true;
                    return;
                }
            }
            if (field.Id == textFieldcreateEmail.Id)
            {
                bool isEmail = Regex.IsMatch(field.EditText?.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                {
                    textFieldcreateEmail.Error = "Must be a valid email address";
                    btnSignup.Clickable = false;
                    return;
                }
                else
                {
                    textFieldcreateEmail.ErrorEnabled = false;
                    btnSignup.Clickable = true;
                    return;
                }
            }
            if (field.Id == textFieldcreatePass.Id)
            {

                if (field.EditText?.Text.Length < 8)
                {
                    textFieldcreatePass.Error = "Password must be 8-10 characters.";
                    btnSignup.Clickable = false;
                    return;
                }
            }
            if (field.Id == textFieldconfirmPass.Id)
            {

                if (field.EditText?.Text != textFieldcreatePass.EditText?.Text)
                {
                    textFieldconfirmPass.Error = "Passwords don't match.";
                    btnSignup.Clickable = false;
                    return;
                }
            }



        }




        private void BtnSignup_Click(object sender, EventArgs e)
        {


            string fullname = textFieldFullname.EditText?.Text;
             string email = textFieldcreateEmail.EditText?.Text;
             string pass = textFieldcreatePass.EditText?.Text;
             string confirmpass = textFieldconfirmPass.EditText?.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
      
                return;
            }

            auth.CreateUserWithEmailAndPassword(email, pass);



        }

        private void LinkSignin_Click(object sender, EventArgs e)
        {
            Intent signIn = new Intent(this, typeof(ActivitySignIn));
            StartActivity(signIn);
            Finish();
        }
    }
}