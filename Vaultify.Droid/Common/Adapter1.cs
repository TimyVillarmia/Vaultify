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
using AndroidX.CardView.Widget;

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
                case "Notes":
                    img_url = "https://cdn-icons-png.freepik.com/512/4021/4021693.png";
                    break;
                case "Visa":
                    img_url = "https://i.pinimg.com/originals/71/fb/1a/71fb1a228e533f9b9bec7164680e0979.png";
                    break;
                case "Mastercard":
                    img_url = "https://banner2.cleanpng.com/20180802/xri/kisspng-logo-mastercard-vector-graphics-font-visa-mastercard-logo-png-photo-png-arts-5b634298cd58d5.9008352515332317688411.jpg";
                    break;
                case "American Express":
                    img_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/American_Express_logo_%282018%29.svg/1200px-American_Express_logo_%282018%29.svg.png";
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




    public class Adapter1ViewHolder : ViewHolder, View.IOnCreateContextMenuListener
    {
        public TextView headline;
        public TextView subheadline;
        public ImageView logo;
        public CardView cardView_recyclerItem;



        public Adapter1ViewHolder(View itemView) : base(itemView)
        {
            headline = (TextView)itemView.FindViewById(Resource.Id.textView_logoname);
            subheadline = (TextView)itemView.FindViewById(Resource.Id.textView_preview);
            logo = (ImageView)itemView.FindViewById(Resource.Id.imageView_logo);
            cardView_recyclerItem = (CardView)itemView.FindViewById(Resource.Id.cardView_recyclerItem);
            cardView_recyclerItem.SetOnCreateContextMenuListener(this);

        }

        public void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            menu.Add(BindingAdapterPosition, 1, 0, "Delete");
            menu.Add(BindingAdapterPosition, 2, 1, "Edit");
        }

    }


}