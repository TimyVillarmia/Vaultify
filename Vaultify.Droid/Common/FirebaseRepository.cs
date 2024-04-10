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

            var options = new FirebaseOptions.Builder()
                .SetProjectId("vaultify-1556e")
                .SetApplicationId("vaultify-1556e")
                .SetApiKey("AIzaSyDOF3-W3yS5DL24QY7fCv9s7VVHHW7BAIU")
                .SetStorageBucket("vaultify-1556e.appspot.com")
                .Build();

            var app = FirebaseApp.InitializeApp(Application.Context, options);

            return FirebaseFirestore.GetInstance(app);
        }

        /*
         Check current auth state
         Check if user is signed in (non-null) and update UI accordingly.

        return 'true' if user is signed 
        otherwise return 'false
         */
        public bool isUserCurrentlySignedIn()
        {
            throw new NotImplementedException();
        }

        /*
         Create a new signIn method which takes in an email address and password,
        validates them, and then signs a user in with the signInWithEmailAndPassword method.
         */
        public void SignIn()
        {
            throw new NotImplementedException();
        }

        /*
         Create a new createAccount method that takes in an email address and password,
        validates them, and then creates a new user with the createUserWithEmailAndPassword method.
         */
        public void createAccount()
        {
            throw new NotImplementedException();
        }

     
    }


    
}