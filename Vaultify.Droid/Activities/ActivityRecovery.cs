using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Google.Android.Material.Button;
using Google.Android.Material.Snackbar;
using Google.Android.Material.TextField;
using Org.Apache.Commons.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityRecovery")]
    public class ActivityRecovery : AppCompatActivity, IOnCompleteListener
    {
        TextView linkSignUp;
        MaterialButton btnSendOTP;
        TextInputLayout textFieldEmail;

        FirebaseAuth auth;

  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.recoveryEmail);
            // Create your application here
            auth = FirebaseRepository.getFirebaseAuth();

            linkSignUp = FindViewById<TextView>(Resource.Id.hyperlink_create);
            btnSendOTP = FindViewById<MaterialButton>(Resource.Id.btnsendcode);
            textFieldEmail = FindViewById<TextInputLayout>(Resource.Id.textFieldEmail);

            linkSignUp.Click += LinkSignUp_Click;
            btnSendOTP.Click += BtnSendOTP_Click;
        }

        private async void BtnSendOTP_Click(object sender, EventArgs e)
        {
            //check first if the email is already registered in the firebase
            // insert firebase code here

            bool isEmail = Regex.IsMatch(textFieldEmail.EditText?.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);


            if (string.IsNullOrEmpty(textFieldEmail.EditText?.Text))
            {
                textFieldEmail.Error = "Must not be empty.";
                return;
            }
            if (!isEmail)
            {
                textFieldEmail.Error = "Must be a valid email address";
                return;
            }
            try
            {
                auth.SendPasswordResetEmail(textFieldEmail.EditText?.Text.Trim())
                    .AddOnCompleteListener(this,this);

                //if (userRecord == null)
                //{
                //    using (var builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this))
                //    {
                //        var title = "There are no Vaultify Accounts associated with this email address.";
                //        builder.SetTitle(title);
                //        builder.SetPositiveButton("OK", (c, ev) =>
                //        {
                //            return;
                //        });
                //        var myCustomDialog = builder.Create();

                //        myCustomDialog.Show();
                //    }
                    
                

                //await Onetimepassword.SendOTP(textFieldEmail.EditText?.Text);

                //Intent accountRecovery = new Intent(this, typeof(ActivityRecoveryCode));
                //StartActivity(accountRecovery);


            }
            catch(FirebaseAuthException ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }


        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                //Toast.MakeText(this, "Email sent, check your ", ToastLength.Short).Show();
                //Intent Signup = new Intent(this, typeof(ActivitySignIn));
                //StartActivity(Signup);
                //Finish();

                using (var builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this))
                {
                    var title = "Reset Password.";
                    var msg = $"We received a request to reset your password for Vaultify. To complete the reset, please check your email: {textFieldEmail.EditText?.Text.Trim()}";
                    builder.SetTitle(title);
                    builder.SetMessage(msg);
                    builder.SetPositiveButton("OK", (c, ev) =>
                    {
                        return;
                    });
                    var myCustomDialog = builder.Create();

                    myCustomDialog.Show();
                }
            }
            else
            {
                Toast.MakeText(this, task.Exception.Message, ToastLength.Short).Show();
            }
        }

        private void LinkSignUp_Click(object sender, EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);
            Finish();
        }
    }
}