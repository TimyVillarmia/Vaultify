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
using Firebase.Firestore.Auth;
using Google.Android.Material.Button;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vaultify.Droid.Common;

namespace Vaultify.Droid.Fragments
{
    public class NoteDialogFragment : DialogFragment, Firebase.Firestore.IEventListener
    {
        FirebaseAuth auth;
        FirebaseUser user;
        FirebaseFirestore db;


        TextView editText_notetitle;
        TextView editText_noteContent;

        string state;
        string current_document;
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
            View view = inflater.Inflate(Resource.Layout.note_dialog_frag, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            MaterialButton btnCancel = (MaterialButton)view.FindViewById(Resource.Id.materialButton1_cancel);
            MaterialButton btnConfirm = (MaterialButton)view.FindViewById(Resource.Id.materialButton_confirm);
            editText_notetitle = (TextView)view.FindViewById(Resource.Id.editText_notetitle);
            editText_noteContent = (TextView)view.FindViewById(Resource.Id.editText_noteContent);

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
            btnConfirm.Click += BtnConfirm_Click; ;

        }

        private void FetchDataListen(string document_id)
        {
            db.Collection("Notes").Document(document_id).AddSnapshotListener(this);


        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            var snapshot = (DocumentSnapshot)obj;

            if (snapshot.Exists())
            {
                Log.Debug("TAG", snapshot.Id);
                editText_notetitle.Text = snapshot.Get("Title").ToString();
                editText_noteContent.Text = snapshot.Get("Content").ToString();

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
                    { "Title", editText_notetitle.Text },
                    { "Content", editText_noteContent.Text },
                };

                FirebaseRepository.FirestoreCloudUpdateDB(
                    FirebaseRepository.getFirebaseDB(),
                    "Notes",
                    current_document,
                    updates);

                Toast.MakeText(Context, "Successfully edited", ToastLength.Long).Show();
            }
            else
            {
                HashMap notes = new HashMap();
                notes.Put("UID", user.Uid);
                notes.Put("Title", editText_notetitle.Text);
                notes.Put("Content", editText_noteContent.Text);


                FirebaseRepository.FirestoreCloudInsertDB(
                    FirebaseRepository.getFirebaseDB(),
                    "Notes",
                    notes);

                Toast.MakeText(Context, "Successfully added to Notes", ToastLength.Long).Show();
            }
                
            Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();
        }

       
    }
}