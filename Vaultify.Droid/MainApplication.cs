using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Vaultify.Droid.Activities;


namespace Vaultify.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainApplication : AppCompatActivity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

           


            // TODO: Check current user signed in
            if (true)
            {
                StartActivity(new Intent(Application.Context, typeof(ActivitySignIn)));
                Finish();

            }

        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}