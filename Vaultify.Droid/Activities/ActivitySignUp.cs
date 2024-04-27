using Android.App;
using Android.Content;
using Android.Gms.Common.Server.Response;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Security;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;

using System.Text.RegularExpressions;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : AppCompatActivity, IOnCompleteListener
    {
        TextView linkSignin;
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
            textFieldcreateEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldcreateEmail);
            textFieldcreatePass = FindViewById<TextInputLayout>(Resource.Id.textFieldcreatePass);
            textFieldconfirmPass = FindViewById<TextInputLayout>(Resource.Id.textFieldconfirmPass);
            btnSignup = FindViewById<MaterialButton>(Resource.Id.btnSignup);

            auth = FirebaseRepository.getFirebaseAuth();




            linkSignin.Click += LinkSignin_Click;
            btnSignup.Click += BtnSignup_Click;

       
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
            //if (field.Id == textFieldFullname.Id)
            //{
            //    if (field.EditText?.Text.Length < 4)
            //    {
            //        textFieldFullname.Error = "Name too short. Please enter at least 4 characters.";
            //        btnSignup.Clickable = false;
            //        return;
            //    }

            //}
            if (field.Id == textFieldcreateEmail.Id)
            {
                bool isEmail = Regex.IsMatch(field.EditText?.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                {
                    textFieldcreateEmail.Error = "Must be a valid email address";
                    btnSignup.Clickable = false;
                    return;
                }

            }
            if (field.Id == textFieldcreatePass.Id)
            {

                if (field.EditText?.Text.Length < 8)
                {
                    textFieldcreatePass.Error = "Password should be at least 6 characters.";
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

            //textFieldFullname.ErrorEnabled = false;
            textFieldcreateEmail.ErrorEnabled = false;
            textFieldcreatePass.ErrorEnabled = false;
            textFieldconfirmPass.ErrorEnabled = false;
            btnSignup.Clickable = true;

        }




        private async void BtnSignup_Click(object sender, EventArgs e)
        {


            //string fullname = textFieldFullname.EditText?.Text;
             string email = textFieldcreateEmail.EditText?.Text;
             string pass = textFieldcreatePass.EditText?.Text;
             string confirmpass = textFieldconfirmPass.EditText?.Text;

            if (string.IsNullOrEmpty(email) || 
                string.IsNullOrEmpty(pass))
            {
                //textFieldFullname.Error = "Must not be empty.";
                textFieldcreateEmail.Error = "Must not be empty.";
                textFieldcreatePass.Error = "Must not be empty.";
                textFieldconfirmPass.Error = "Must not be empty.";

                return;
            }


            auth.CreateUserWithEmailAndPassword(email, pass)
                  .AddOnCompleteListener(this, this);



        }

        private void StartSignInActivity()
        {
            Intent signIn = new Intent(this, typeof(ActivitySignIn));
            StartActivity(signIn);
            Finish();
        }

        private void LinkSignin_Click(object sender, EventArgs e)
        {
            StartSignInActivity();
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Registration was successful!", ToastLength.Short).Show();
                StartSignInActivity();
                Finish();
            }
            else
            {
                Toast.MakeText(this, task.Exception.Message, ToastLength.Short).Show();
            }
        }
    }
}