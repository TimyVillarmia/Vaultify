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
using Square.Picasso;
using Vaultify.Droid.Fragments;

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
            string img_url = "";

            Adapter1ViewHolder myHolder = holder as Adapter1ViewHolder;
            CardModel currentModel = cardList[position];
            myHolder.headline.Text = currentModel.Row_Headline;
            myHolder.subheadline.Text = currentModel.Row_SubHeadline;

            switch (currentModel.Row_Image)
            {
                case "Google":
                    img_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Google_%22G%22_logo.svg/1200px-Google_%22G%22_logo.svg.png";
                    break;
                case "Facebook":
                    img_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/2023_Facebook_icon.svg/1024px-2023_Facebook_icon.svg.png";
                    break;
                case "LinkedIn":
                    img_url = "https://upload.wikimedia.org/wikipedia/commons/e/e8/Linkedin-logo-blue-In-square-40px.png";
                    break;
                case "Tiktok":
                    img_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Ionicons_logo-tiktok.svg/512px-Ionicons_logo-tiktok.svg.png?20230423144016";
                    break;
            }

            if (!string.IsNullOrEmpty(img_url))
            {
                Picasso
                    .Get()
                    .Load(img_url)
                    .Fit()
                    .CenterCrop()
                    .NoFade()
                    .Into(myHolder.logo);
            }


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
        public TextView headline;
        public TextView subheadline;
        public ImageView logo;

        public Adapter1ViewHolder(View itemView) : base(itemView)
        {
            headline = (TextView)itemView.FindViewById(Resource.Id.textView_logoname);
            subheadline = (TextView)itemView.FindViewById(Resource.Id.textView_preview);
            logo = (ImageView)itemView.FindViewById(Resource.Id.imageView_logo);
        }
    }
}