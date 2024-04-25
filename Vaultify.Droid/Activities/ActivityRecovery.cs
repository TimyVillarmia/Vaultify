using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using Google.Android.Material.Snackbar;
using Google.Android.Material.TextField;
using Org.Apache.Commons.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityRecovery")]
    public class ActivityRecovery : AppCompatActivity
    {
        TextView linkSignUp;
        MaterialButton btnSendOTP;
        TextInputLayout textFieldEmail;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.accountRecovery);
            // Create your application here

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

            bool emailisvalid = true;

            try
            {
                if (emailisvalid)
                {
                    await Onetimepassword.SendOTP(textFieldEmail.EditText?.Text);
                    View view = (View)sender;
                    Snackbar.Make(view, "OTP has been sent", Snackbar.LengthLong);

                    Intent accountRecovery = new Intent(this, typeof(ActivityRecoveryCode));
                    StartActivity(accountRecovery);
                    
                }
                else
                {
                    using (var builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this))
                    {
                        var title = "There are no Vaultify Accounts associated with this email address.";
                        builder.SetTitle(title);
                        builder.SetPositiveButton("OK", (c, ev) => 
                        { 

                        });
                        var myCustomDialog = builder.Create();

                        myCustomDialog.Show();
                    }

                }

            }
            catch(Exception)
            {
                View view = (View)sender;
                Snackbar.Make(view, "Something went wrong", Snackbar.LengthLong);
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