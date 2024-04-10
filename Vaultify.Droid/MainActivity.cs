using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Fragment;
using AndroidX.Fragment.App;
using AndroidX.Transitions;
using Vaultify.Droid.Activities;
using Vaultify.Droid.Resources.layout;

namespace Vaultify.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView signuptext;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.signin);


            //SupportFragmentManager.BeginTransaction()
            //.Add(Resource.Id.frameLayout, new LoginFragment())
            //.Commit();
            signuptext = FindViewById<TextView>(Resource.Id.hyperlink_create);

            signuptext.Click += Signup_Click;
            
            
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