﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaultify.Droid.Common
{
    internal interface IFirebase
    {
        bool isUserCurrentlySignedIn();
        void SignIn();
        void createAccount();
        
        
    }
}