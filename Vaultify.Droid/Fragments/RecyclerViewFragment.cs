using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AndroidX.RecyclerView.Widget.RecyclerView;
using AndroidX.Fragment.App;
using AndroidX.RecyclerView.Widget;
using Vaultify.Droid.Common;
using Vaultify.Droid.Common.Models;
using AndroidX.AppCompat.App;
using Vaultify.Droid.Activities;
using Firebase.Firestore;
using Firebase.Auth;
using AndroidX.CardView.Widget;
using AndroidX.Activity.ContextAware;
using Google.Android.Material.Snackbar;
using Android.Gms.Tasks;
using System.Threading.Tasks;
using static Android.Icu.Text.Transliterator;
using Java.Util;

namespace Vaultify.Droid.Fragments
{

    public class RecyclerViewFragment : Fragment, Firebase.Firestore.IEventListener, IOnSuccessListener, IOnFailureListener
    {

        List<CardModel> cardlist = new List<CardModel>();
        Adapter1 itemAdapter;

        FirebaseFirestore db;
        FirebaseAuth auth;
        FirebaseUser user;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
            db = FirebaseRepository.getFirebaseDB();
            auth = FirebaseRepository.getFirebaseAuth();
            user = auth.CurrentUser;

            FetchDataListen();
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.recyclerview_frag, container, false);
            return view;
        }



        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            //((ActivityHome)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //((ActivityHome)Activity).SupportActionBar.SetDisplayShowHomeEnabled(true);

            // Assign employeelist to ItemAdapter
            itemAdapter = new Adapter1(cardlist);
            // Set the LayoutManager that
            // this RecyclerView will use.
            RecyclerView recyclerView
                = (RecyclerView)view.FindViewById(Resource.Id.recycleView);
            recyclerView.SetLayoutManager(
                new LinearLayoutManager(Context));
            // adapter instance is set to the
            // recyclerview to inflate the items.
            recyclerView.SetAdapter(itemAdapter);
        }




        public override bool OnContextItemSelected(IMenuItem item)
        {
            Bundle bundle = new Bundle();
    

            switch (item.ItemId)
            {
                case 1:
                    RemoveDataListen(cardlist[item.GroupId].Id);
                    break;
                case 2:

                    if (ContentMainFragment.QueryString == "Logins")
                    {
                        LoginsDialogFragment fragment = new LoginsDialogFragment();
                        bundle.PutString("State", "Edit");
                        bundle.PutString("Current_Document", cardlist[item.GroupId].Id);
                        fragment.Arguments = bundle;
                        ((ActivityHome)Activity).ShowDialog(fragment);

                    }
                    if (ContentMainFragment.QueryString == "Notes")
                    {
                        NoteDialogFragment fragment = new NoteDialogFragment();
                        bundle.PutString("State", "Edit");
                        bundle.PutString("Current_Document", cardlist[item.GroupId].Id);
                        fragment.Arguments = bundle;
                        ((ActivityHome)Activity).ShowDialog(fragment);
                    }

                    if (ContentMainFragment.QueryString == "Cards")
                    {
                        CreditDialogFragment fragment = new CreditDialogFragment();
                        bundle.PutString("State", "Edit");
                        bundle.PutString("Current_Document", cardlist[item.GroupId].Id);
                        fragment.Arguments = bundle;
                        ((ActivityHome)Activity).ShowDialog(fragment);
                    }




                    break;
            }

            return true;
        }
        private void RemoveDataListen(string document_id)
        {
            db.Collection(ContentMainFragment.QueryString).Document(document_id).Delete()
                .AddOnSuccessListener(this)
                .AddOnFailureListener(this);

        }


        private void FetchDataListen()
        {
            db.Collection(ContentMainFragment.QueryString).WhereEqualTo("UID", user.Uid).AddSnapshotListener(this);

        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            var snapshot = (QuerySnapshot)obj;

            if (error != null)
            {
                Log.Debug("X", "Listen failed.", error);
                return;
            }

            if (!snapshot.IsEmpty)
            {
                cardlist.Clear();
                var documents = snapshot.Documents;

                foreach (var item in documents)
                {
                    CardModel cardmodel = new CardModel();

                    var query = ContentMainFragment.QueryString;
                    switch (query)
                    {
                        case "AllItems":
                        //cardmodel.Row_Headline = item.Get("Website").ToString();
                        //cardmodel.Row_SubHeadline = item.Get("Email").ToString();
                        //cardmodel.Row_Image = item.Get("Website").ToString();
                        //break;
                        case "Notes":
                            cardmodel.Id = item.Id;
                            cardmodel.Row_Headline = item.Get("Title").ToString();
                            cardmodel.Row_SubHeadline = item.Get("Content").ToString();
                            cardmodel.Row_Image = "Notes";
                            break;
                        case "Cards":
                            cardmodel.Id = item.Id;
                            cardmodel.Row_Headline = item.Get("Type").ToString();
                            cardmodel.Row_SubHeadline = item.Get("Account Holder").ToString();
                            cardmodel.Row_Image = item.Get("Type").ToString();
                            break;
                        case "Logins":
                            cardmodel.Id = item.Id;
                            cardmodel.Row_Headline = item.Get("Website").ToString();
                            cardmodel.Row_SubHeadline = item.Get("Email").ToString();
                            cardmodel.Row_Image = item.Get("Website").ToString();
                            break;
                    }

                    cardlist.Add(cardmodel);

                }

                if (itemAdapter != null)
                {
                    itemAdapter.NotifyDataSetChanged();
                }
            }
            else
            {
                Log.Debug("TAG", "Current data: null");
            }


        }

        public void OnSuccess(Java.Lang.Object result)
        {
            itemAdapter.NotifyDataSetChanged();


            Toast.MakeText(Activity, "Successfully deleted", ToastLength.Short).Show();
            Log.Debug("X", "DocumentSnapshot successfully deleted!");
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(Activity, "Something went wrong", ToastLength.Short).Show();
            Log.Debug("X", "Error deleting document", e);
        }
    }
}