using System;
using System.Linq;
using Android.Animation;
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
using AndroidX.Fragment.App;
using Firebase.Auth;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Vaultify.Droid.Common;
using Vaultify.Droid.Fragments;

namespace Vaultify.Droid.Activities
{
    [Activity(Label = "ActivityHome")]
    public class ActivityHome : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private static bool isFabOpen;

        Button button_allitems;
        Button button_notes;
        Button button_logins;
        Button button_credits;
        TextView textView_placeholder;
        FloatingActionButton fabNotes;
        FloatingActionButton fabCredit;
        FloatingActionButton fabLogin;
        FloatingActionButton fab;

        FirebaseAuth auth;
        AndroidX.Fragment.App.FragmentManager fragmentManager;
        public AndroidX.AppCompat.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.home);
            auth = FirebaseRepository.getFirebaseAuth();
            FirebaseUser user = auth.CurrentUser;

            fragmentManager = SupportFragmentManager;


            fabNotes = FindViewById<FloatingActionButton>(Resource.Id.fab_notes);
            fabCredit = FindViewById<FloatingActionButton>(Resource.Id.fab_credit);
            fabLogin = FindViewById<FloatingActionButton>(Resource.Id.fab_login);
            button_allitems = FindViewById<Button>(Resource.Id.button_allitems);
            button_notes = FindViewById<Button>(Resource.Id.button_notes);
            button_logins = FindViewById<Button>(Resource.Id.button_logins);
            button_credits = FindViewById<Button>(Resource.Id.button_credits);
            textView_placeholder = FindViewById<TextView>(Resource.Id.textView_placeholder);

            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

     
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener((NavigationView.IOnNavigationItemSelectedListener)this);

            // insert default fragment
            ReplaceFragment(new ContentMainFragment(), "Default");


            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            fab.Click += (o, e) =>
            {
                if (!isFabOpen)
                    ShowFabMenu();
                else
                    CloseFabMenu();
            };

            fabNotes.Click += (o, e) =>
            {
                CloseFabMenu();
                ShowDialog(new NoteDialogFragment());
            };

            fabCredit.Click += (o, e) =>
            {
                CloseFabMenu();
                ShowDialog(new CreditDialogFragment());

            };
            fabLogin.Click += (o, e) =>
            {
                CloseFabMenu();
                ShowDialog(new LoginsDialogFragment());

            };



        }
        
        private void ShowFabMenu()
        {
            isFabOpen = true;
            fabNotes.Visibility = ViewStates.Visible;
            fabCredit.Visibility = ViewStates.Visible;
            fabLogin.Visibility = ViewStates.Visible;

            fab.Animate().Rotation(135f);
            fabNotes.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_145))
                .Rotation(0f);
            fabCredit.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_100))
                .Rotation(0f);
            fabLogin.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_55))
                .Rotation(0f);
        }

        private void CloseFabMenu()
        {
            isFabOpen = false;

            fab.Animate().Rotation(0f);
            fabNotes.Animate()
                .TranslationY(0f)
                .Rotation(90f);
            fabCredit.Animate()
                .TranslationY(0f)
                .Rotation(90f);
            fabLogin.Animate()
                .TranslationY(0f)
                .Rotation(90f).SetListener(new FabAnimatorListener(fabLogin, fabCredit, fabNotes));
        }

        private class FabAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            View[] viewsToHide;

            public FabAnimatorListener(params View[] viewsToHide)
            {
                this.viewsToHide = viewsToHide;
            }

            public void OnAnimationCancel(Animator animation)
            {
            }

            public void OnAnimationEnd(Animator animation)
            {
                if (!isFabOpen)
                    foreach (var view in viewsToHide)
                        view.Visibility = ViewStates.Gone;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
            }
        }

        public void ShowDialog(AndroidX.Fragment.App.DialogFragment dialogFragment)
        {
            fragmentManager = SupportFragmentManager;
            AndroidX.Fragment.App.FragmentTransaction transaction = fragmentManager.BeginTransaction();
            transaction.SetTransition(AndroidX.Fragment.App.FragmentTransaction.TransitFragmentOpen);
            // To make it fullscreen, use the 'content' root view as the container
            // for the fragment, which is always the root view for the activity.
            transaction.Add(Android.Resource.Id.Content, dialogFragment)
                       .AddToBackStack(null).Commit();
        }

        public void ReplaceFragment(AndroidX.Fragment.App.Fragment fragment, string tag)
        {
            fragmentManager = SupportFragmentManager;


            fragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout_fragment, fragment, tag)
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
            else if (id == Resource.Id.homeAsUp)
            {
                return true;
            }


            return base.OnOptionsItemSelected(item);
        }



        [Obsolete]
        public override void OnBackPressed()
        {

            if (!(fragmentManager.Fragments.First() is ContentMainFragment))
            {

                ReplaceFragment(new ContentMainFragment(), "Default");
                toolbar.SetTitle(Resource.String.app_name);
            }
            else
            {
                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                if (drawer.IsDrawerOpen(GravityCompat.Start))
                {
                    drawer.CloseDrawer(GravityCompat.Start);
                }
                else
                {
                    using (var builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this))
                    {
                        var title = "Exit";
                        var msg = "Are you sure you want to exit?";
                        builder.SetTitle(title);
                        builder.SetMessage(msg);
                        builder.SetPositiveButton("Confirm", (c, ev) =>
                        {
                            FinishAffinity();
                        });
                        builder.SetNegativeButton("Cancel", (c, ev) =>
                        {
                            return;

                        });
                        var myCustomDialog = builder.Create();

                        myCustomDialog.Show();
                    }
                }
            }


            



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