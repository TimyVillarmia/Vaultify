using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Firestore.Auth;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaultify.Droid.Common.Models;
using Vaultify.Droid.Fragments;

namespace Vaultify.Droid.Common
{
    public class FirebaseRepository
    {

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

            return FirebaseAuth.GetInstance(app);
        }

        public static FirebaseFirestore getFirebaseDB()
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

            return FirebaseFirestore.GetInstance(app);
        }

        public static async void FirestoreCloudInsertDB(FirebaseFirestore db, string collection, HashMap payload)
        {
            DocumentReference docRef = db.Collection(collection).Document();
            payload.Put("Id", docRef.Id);
            await docRef.Set(payload);
        }

        public static async void FirestoreCloudUpdateDB(FirebaseFirestore db, string collection, string document_id, Dictionary<string, Java.Lang.Object> updates)
        {

            DocumentReference docRef = db.Collection(collection).Document(document_id);
            await docRef.Update(updates);
        }
        public static DocumentReference FetchData(FirebaseFirestore db, string collection, string user_id)
        {
            DocumentReference docRef = db.Collection(collection).Document(user_id);
            return docRef;
        }



    }



}