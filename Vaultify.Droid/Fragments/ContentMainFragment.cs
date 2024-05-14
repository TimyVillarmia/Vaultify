using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using Firebase.Auth;
using Google.Android.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Activities;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Fragments
{
    public class ContentMainFragment : Fragment
    {
        FirebaseAuth auth;
        FirebaseUser user;

        public static string QueryString { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            auth = FirebaseRepository.getFirebaseAuth();
            user = auth.CurrentUser;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.content_main, container, false);
            return view;

        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);



            Button button_allitems = view.FindViewById<Button>(Resource.Id.button_allitems);
            Button button_notes = view.FindViewById<Button>(Resource.Id.button_notes);
            Button button_logins = view.FindViewById<Button>(Resource.Id.button_logins);
            Button button_credits = view.FindViewById<Button>(Resource.Id.button_credits);

            TextView textView_placeholder = view.FindViewById<TextView>(Resource.Id.textView_placeholder);
            textView_placeholder.Text = user.Email;

            button_allitems.Click += Button_allitems_Click;

            button_notes.Click += Button_notes_Click;
            button_logins.Click += Button_logins_Click;
            button_credits.Click += Button_credits_Click;

        }
        
  






        private void Button_credits_Click(object sender, EventArgs e)
        {
            ((ActivityHome)Activity).ReplaceFragment(new RecyclerViewFragment(), "Recycler");
            ((ActivityHome)Activity).toolbar.SetTitle(Resource.String.appbar_title_credits);
            QueryString = "Cards";
        }

        private void Button_logins_Click(object sender, EventArgs e)
        {
            ((ActivityHome)Activity).ReplaceFragment(new RecyclerViewFragment(), "Recycler");
            ((ActivityHome)Activity).toolbar.SetTitle(Resource.String.appbar_title_logins);
            QueryString = "Logins";
        }

        private void Button_notes_Click(object sender, EventArgs e)
        {
            ((ActivityHome)Activity).ReplaceFragment(new RecyclerViewFragment(), "Recycler");
            ((ActivityHome)Activity).toolbar.SetTitle(Resource.String.appbar_title_notes);
            QueryString = "Notes";
        }

        private void Button_allitems_Click(object sender, EventArgs e)
        {
            ((ActivityHome)Activity).ReplaceFragment(new RecyclerViewFragment(), "Recycler");
            ((ActivityHome)Activity).toolbar.SetTitle(Resource.String.appbar_title_allitems);
            QueryString = "AllItems";
        }
    }
}