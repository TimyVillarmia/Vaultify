using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Google.Android.Material.Button;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaultify.Droid.Activities;
using Vaultify.Droid.Common;
using Vaultify.Droid.Common.Models;

namespace Vaultify.Droid.Fragments
{
    public class LoginsDialogFragment : DialogFragment, Firebase.Firestore.IEventListener
    {

        EditText editText_newemail;
        EditText editText_newpassword;
        Spinner spinner;

        FirebaseAuth auth;
        FirebaseUser user;
        FirebaseFirestore db;

        string state;
        string current_document;
        ArrayAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            auth = FirebaseRepository.getFirebaseAuth();
            user = auth.CurrentUser;
            db = FirebaseRepository.getFirebaseDB();


            Bundle bundle = this.Arguments;
            if (bundle != null)
            {
                state = bundle.GetString("State", "Create");
                current_document = bundle.GetString("Current_Document", "");
            }
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
            adapter = ArrayAdapter.CreateFromResource(
                    Context, Resource.Array.websites_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            if (state == "Edit")
            {
                btnConfirm.Text = "Save";
                FetchDataListen(current_document);

            }
            else
            {
                btnConfirm.Text = "Confirm";

            }


            btnCancel.Click += (o, e) =>
            {
                Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();
            };
            btnConfirm.Click += BtnConfirm_Click;

        }

        private void FetchDataListen(string document_id)
        {
            db.Collection("Logins").Document(document_id).AddSnapshotListener(this);


        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            var snapshot = (DocumentSnapshot)obj;

            if (snapshot.Exists())
            {
                Log.Debug("TAG", snapshot.Id);
                editText_newemail.Text = snapshot.Get("Email").ToString();
                editText_newpassword.Text = snapshot.Get("Password").ToString();
                spinner.SetSelection(adapter.GetPosition(snapshot.Get("Website").ToString()));

            }
            else
            {
                Log.Debug("Document {0} does not exist!", snapshot.Id);

            }
        }


        private void BtnConfirm_Click(object sender, EventArgs e)
        {

            if (state == "Edit")
            {

                Dictionary<string, Java.Lang.Object> updates = new Dictionary<string, Java.Lang.Object>
                {
                    { "Email", editText_newemail.Text },
                    { "Password", editText_newpassword.Text },
                    { "Website",  spinner.SelectedItem }
                };

                FirebaseRepository.FirestoreCloudUpdateDB(
                    FirebaseRepository.getFirebaseDB(),
                    "Logins",
                    current_document,
                    updates);

                Toast.MakeText(Context, "Successfully edited", ToastLength.Long).Show();
            }
            else
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
            }

            Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();


        }



        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

        }

     
    }
}