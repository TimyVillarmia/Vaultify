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

namespace Vaultify.Droid.Fragments
{

    public class RecyclerViewFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
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


            List<CardModel> cardlist = Constants.getEmployeeData();
            // Assign employeelist to ItemAdapter
            Adapter1 itemAdapter = new Adapter1(cardlist);
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


    }
}