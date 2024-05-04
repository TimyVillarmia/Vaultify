using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Firebase.Auth;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Vaultify.Droid.Common;
using Vaultify.Droid.Fragments;
using static Android.Content.ClipData;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityHome")]
    public class ActivityHome : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {

        Button button_allitems;
        Button button_notes;
        Button button_logins;
        Button button_credits;
        TextView textView_placeholder;

        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.home);
            auth = FirebaseRepository.getFirebaseAuth();
            FirebaseUser user = auth.CurrentUser;



            button_allitems = FindViewById<Button>(Resource.Id.button_allitems);
            button_notes = FindViewById<Button>(Resource.Id.button_notes);
            button_logins = FindViewById<Button>(Resource.Id.button_logins);
            button_credits = FindViewById<Button>(Resource.Id.button_credits);
            textView_placeholder = FindViewById<TextView>(Resource.Id.textView_placeholder);

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
     

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener((NavigationView.IOnNavigationItemSelectedListener)this);

            //ReplaceFragment(new DefaultFragment(), "Default");
            textView_placeholder.Text = user.Email;
            button_allitems.Click += delegate { ReplaceFragment(new RecyclerViewFragment(), "Recycler"); };
        }

        private void ReplaceFragment(AndroidX.Fragment.App.Fragment fragment, string tag)
        {
            var fragmentManager = SupportFragmentManager;

            var fragmentTransaction = fragmentManager.BeginTransaction();

            fragmentTransaction.Replace(Resource.Id.frameLayout_fragment, fragment, tag)
                .Commit();
        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        [Obsolete]
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {

                base.OnBackPressed();

            }
        }


        private void FabOnClick(object sender, EventArgs e)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.logout)
            {
                using (var builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this))
                {
                    var title = "Logout";
                    var msg = "Are you sure you want to logout out?";
                    builder.SetTitle(title);
                    builder.SetMessage(msg);
                    builder.SetPositiveButton("Confirm", (c, ev) =>
                    {
                        auth.SignOut();
                        StartActivity(new Intent(Application.Context, typeof(ActivitySignIn)));
                        Finish();
                    });
                    builder.SetNegativeButton("Cancel", (c, ev) =>
                    {
                        return; 

                    });
                    var myCustomDialog = builder.Create();

                    myCustomDialog.Show();
                }
            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}