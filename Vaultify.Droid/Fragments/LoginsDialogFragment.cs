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
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Fragments
{
    public class LoginsDialogFragment : DialogFragment
    {

        EditText editText_newemail;
        EditText editText_newpassword;
        Spinner spinner;

        FirebaseAuth auth;
        FirebaseUser user;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            auth = FirebaseRepository.getFirebaseAuth();
            user = auth.CurrentUser;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.login_dialog_frag, container, false);
            return view;

        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            MaterialButton btnCancel = (MaterialButton)view.FindViewById(Resource.Id.materialButton1_cancel);
            MaterialButton btnConfirm = (MaterialButton)view.FindViewById(Resource.Id.materialButton_confirm);

            editText_newemail = (EditText)view.FindViewById(Resource.Id.editText_newemail);
            editText_newpassword = (EditText)view.FindViewById(Resource.Id.editText_newpassword);
            spinner = view.FindViewById<Spinner>(Resource.Id.spinner_website);


            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    Context, Resource.Array.websites_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;


            btnCancel.Click += (o, e) =>
            {
                Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();
            };
            btnConfirm.Click += BtnConfirm_Click;

        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            HashMap logins = new HashMap();
            logins.Put("UID", user.Uid);
            logins.Put("Email", editText_newemail.Text);
            logins.Put("Password", editText_newpassword.Text);
            logins.Put("Website", spinner.SelectedItem);


            FirebaseRepository.FirestoreCloudInsertDB(
                FirebaseRepository.getFirebaseDB(),
                "Logins",
                logins);

            Toast.MakeText(Context, "Successfully added to Logins", ToastLength.Long).Show();
            Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();


        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

        }
    }
}