using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vaultify.Droid.Common.Models
{
    public class CardModel
    {
        private string logoName; 
        private string previewText;  

        public CardModel(string logoName, string previewText)
        {
            this.logoName = logoName;
            this.previewText = previewText;
        }

        public string getlogoName()
        {
            return logoName;
        }

        public string getpreviewText()
        {
            return previewText;
        }
    }
}