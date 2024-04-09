using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaultify.Droid.Resources.layout
{
    public class LoginFragment : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
         
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            // Use this to return your custom view for this Fragment
            var fragmentView = inflater.Inflate(Resource.Layout.signin, container, false);
            return fragmentView;
        }
    }
}