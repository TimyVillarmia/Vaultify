using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Firebase.Auth;
using Firebase.Firestore;
using Google.Android.Material.Button;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vaultify.Droid.Common;
using static Android.Provider.CalendarContract;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Grpc.NameResolver;

namespace Vaultify.Droid.Fragments
{
    public class CreditDialogFragment : DialogFragment, Firebase.Firestore.IEventListener
    {
        FirebaseAuth auth;
        FirebaseUser user;
        FirebaseFirestore db;

        TextView editText_holder;
        TextView editText_cardnumber;
        TextView editText_securitycode;
        TextView editText_expirydate;
        Spinner spinner;

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
            View view = inflater.Inflate(Resource.Layout.credit_dialog_frag, container, false);
            return view;
        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            MaterialButton btnCancel = (MaterialButton)view.FindViewById(Resource.Id.materialButton1_cancel);
            MaterialButton btnConfirm = (MaterialButton)view.FindViewById(Resource.Id.materialButton_confirm);

            editText_holder = (TextView)view.FindViewById(Resource.Id.editText_holder);
            editText_cardnumber = (TextView)view.FindViewById(Resource.Id.editText_cardnumber);
            editText_securitycode = (TextView)view.FindViewById(Resource.Id.editText_securitycode);
            editText_expirydate = (TextView)view.FindViewById(Resource.Id.editText_expirydate);
            spinner = view.FindViewById<Spinner>(Resource.Id.spinner_card_type);

            adapter = ArrayAdapter.CreateFromResource(
                    Context, Resource.Array.card_type_array, Android.Resource.Layout.SimpleSpinnerItem);

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
            editText_expirydate.TextChanged += EditText_expirydate_TextChanged;

        }

        private void FetchDataListen(string document_id)
        {
            db.Collection("Cards").Document(document_id).AddSnapshotListener(this);


        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            var snapshot = (DocumentSnapshot)obj;

            if (snapshot.Exists())
            {

                Log.Debug("TAG", snapshot.Id);

                spinner.SetSelection(adapter.GetPosition(snapshot.Get("Type").ToString()));
                editText_holder.Text = snapshot.Get("Account Holder").ToString();
                editText_cardnumber.Text = snapshot.Get("Card Number").ToString();
                editText_securitycode.Text = snapshot.Get("Security Code").ToString();
                editText_expirydate.Text = snapshot.Get("Expiry Date").ToString();

            }
            else
            {
                Log.Debug("Document {0} does not exist!", snapshot.Id);

            }
        }

        private void EditText_expirydate_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var editText = (EditText)sender;

            // get current text
            var text = editText.Text;

            // Remove non-digit characters and apply MM/YY format
            text = Regex.Replace(text, "[^0-9]", "");

            // Check is MM is valid 01 to 12
            if (text.Length > 1 && !Regex.IsMatch(text.Substring(0,2), @"^(0[1-9]|1[0-2])$"))
            {
                // if not remove second digit of MM
                // example: 13, removes 3 from text
                text = text.Substring(0, 1);
            }

                
            if (text.Length > 2)
            {
                // add slash after valid MM 
                text = text.Insert(2, "/");
            }
            
            

            // Ensure maximum length of 5 (MM/YY)
            if (text.Length > 5)
            {
                text = text.Substring(0, 5);
            }

            // Update the EditText if the formatted text is different
            if (editText.Text != text)
            {
                editText.Text = text;
                editText.SetSelection(text.Length); // Move cursor to the end
            }

        }


        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (state == "Edit")
            {

                Dictionary<string, Java.Lang.Object> updates = new Dictionary<string, Java.Lang.Object>
                {
                    { "Type", spinner.SelectedItem },
                    { "Account Holder", editText_holder.Text },
                    { "Card Number", editText_cardnumber.Text },
                    { "Security Code", editText_securitycode.Text },
                    { "Expiry Date",  editText_expirydate.Text }
                };

                FirebaseRepository.FirestoreCloudUpdateDB(
                    FirebaseRepository.getFirebaseDB(),
                    "Cards",
                    current_document,
                    updates);

                Toast.MakeText(Context, "Successfully edited", ToastLength.Long).Show();
            }
            else
            {
                HashMap credits = new HashMap();
                credits.Put("UID", user.Uid);
                credits.Put("Type", spinner.SelectedItem);
                credits.Put("Account Holder", editText_holder.Text);
                credits.Put("Card Number", editText_cardnumber.Text);
                credits.Put("Security Code", editText_securitycode.Text);
                credits.Put("Expiry Date", editText_expirydate.Text);


                FirebaseRepository.FirestoreCloudInsertDB(
                    FirebaseRepository.getFirebaseDB(),
                    "Cards",
                    credits);

                Toast.MakeText(Context, "Successfully added to Credits", ToastLength.Long).Show();
            }
            
            Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();
        }

     
    }
}