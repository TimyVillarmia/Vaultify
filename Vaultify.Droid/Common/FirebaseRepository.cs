using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaultify.Droid.Common
{
    public class FirebaseRepository : IFirebase
    {
        FirebaseFirestore auth;

        public FirebaseRepository()
        {
            auth = getFirebaseAuth();

        }

 

        private FirebaseFirestore getFirebaseAuth()
        {
            FirebaseFirestore database;

            var options = new FirebaseOptions.Builder()
                .SetProjectId("vaultify-1556e")
                .SetApplicationId("vaultify-1556e")
                .SetApiKey("AIzaSyDOF3-W3yS5DL24QY7fCv9s7VVHHW7BAIU")
                .SetStorageBucket("vaultify-1556e.appspot.com")
                .Build();

            var app = FirebaseApp.InitializeApp(Application.Context, options);
            database = FirebaseFirestore.GetInstance(app);

            return database;
        }


        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }
    }


    
}