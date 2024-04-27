using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Vaultify.Droid.Activities;
using Vaultify.Droid.Common;


namespace Vaultify.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainApplication : AppCompatActivity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            FirebaseAuth auth = FirebaseRepository.getFirebaseAuth();

            // TODO: Check current user signed in
            FirebaseUser user = auth.CurrentUser;
            if (user != null)
            {
                StartActivity(new Intent(Application.Context, typeof(ActivityHome)));
                Finish();
            }
            else
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