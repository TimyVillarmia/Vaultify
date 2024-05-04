using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.RecyclerView.Widget;
using Vaultify.Droid.Common.Models;
using static AndroidX.RecyclerView.Widget.RecyclerView;
using Android.Widget;
using Android.Views;
using static Java.Util.Jar.Attributes;

namespace Vaultify.Droid.Common
{
    public class Adapter1 : RecyclerView.Adapter
    {

        private List<CardModel> cardList;

        public Adapter1(List<CardModel> list)
        {
            this.cardList = list;
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override void OnBindViewHolder(ViewHolder holder, int position)
        {

            Adapter1ViewHolder myHolder = holder as Adapter1ViewHolder;
            CardModel currentModel = cardList[position];
            myHolder.logoname.Text = currentModel.getlogoName();
            myHolder.logoname.Text = currentModel.getpreviewText();
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.items_list, parent, false);
            return new Adapter1ViewHolder(itemView);
        }

        public override int ItemCount => cardList.Count;
    }

    public class Adapter1ViewHolder : ViewHolder
    {
        public TextView logoname;
        public TextView previewtext;

        public Adapter1ViewHolder(View itemView) : base(itemView)
        {
            logoname = (TextView)itemView.FindViewById(Resource.Id.textView_logoname);
            previewtext = (TextView)itemView.FindViewById(Resource.Id.textView_preview);
        }
    }
}