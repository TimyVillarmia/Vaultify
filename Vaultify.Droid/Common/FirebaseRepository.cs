using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaultify.Droid.Common
{
    public class FirebaseRepository
    {

        public FirebaseRepository()
        {

        }



        public static FirebaseAuth getFirebaseAuth()
        {          
            //app instance
            var app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                   .SetProjectId("vaultify-1556e")
                   .SetApplicationId("vaultify-1556e")
                   .SetApiKey("AIzaSyDOF3-W3yS5DL24QY7fCv9s7VVHHW7BAIU")
                   .SetStorageBucket("vaultify-1556e.appspot.com")
                   .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);

            }




            return FirebaseAuth.Instance;
        }


     
    }


    
}