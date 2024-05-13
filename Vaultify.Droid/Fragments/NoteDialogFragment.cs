using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Firebase.Auth;
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
    public class NoteDialogFragment : DialogFragment
    {
        FirebaseAuth auth;
        FirebaseUser user;

        TextView editText_notetitle;
        TextView editText_noteContent;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            auth = FirebaseRepository.getFirebaseAuth();
            user = auth.CurrentUser;
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

            btnCancel.Click += (o, e) =>
            {
                Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();
            };
            btnConfirm.Click += BtnConfirm_Click; ;

        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            HashMap notes = new HashMap();
            notes.Put("Title", editText_notetitle.Text);
            notes.Put("Content", editText_noteContent.Text);


            FirebaseRepository.FirestoreCloudInsertDB(
                FirebaseRepository.getFirebaseDB(),
                "Notes",
                user.Uid,
                notes);

            Toast.MakeText(Context, "Successfully added to Notes", ToastLength.Long).Show();
            Activity.SupportFragmentManager.BeginTransaction().Remove(this).Commit();
        }
    }
}