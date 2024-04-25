using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityRecoveryCode")]
    public class ActivityRecoveryCode : Activity
    {
        TextInputLayout textFieldOTPCode;
        TextView hyperlink_resendcode;
        TextView hyperlink_create;
        MaterialButton btnCancel;   
        MaterialButton btnConfirm;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.inputtingCode);
            // Create your application here

             textFieldOTPCode = FindViewById<TextInputLayout>(Resource.Id.textFieldOTPCode);
             hyperlink_resendcode = FindViewById<TextView>(Resource.Id.hyperlink_resendcode);
            hyperlink_create = FindViewById<TextView>(Resource.Id.hyperlink_create);
            btnCancel = FindViewById<MaterialButton>(Resource.Id.btnCancel);
            btnConfirm = FindViewById<MaterialButton>(Resource.Id.btnConfirm);


            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            hyperlink_resendcode.Click += Hyperlink_resendcode_Click;
            hyperlink_create.Click += Hyperlink_create_Click;
        }

        private void Hyperlink_create_Click(object sender, EventArgs e)
        {
            Intent Signup = new Intent(this, typeof(ActivitySignUp));
            StartActivity(Signup);
            Finish();
        }

        private async void Hyperlink_resendcode_Click(object sender, EventArgs e)
        {
            await Onetimepassword.ResendOTP();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Intent accountRecovery = new Intent(this, typeof(ActivityRecovery));
            StartActivity(accountRecovery);
            Finish();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (textFieldOTPCode.EditText?.Text ==  Onetimepassword.OTP)
            {
                //Intent accountRecovery = new Intent(this, typeof(ActivityRes));
                //StartActivity(accountRecovery);
            }
            else
            {
                //error 
            }
        }
    }
}