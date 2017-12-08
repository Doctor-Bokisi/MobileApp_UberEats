﻿using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using UBER.Models;
using Android.Graphics;
using Android.Views;
using Android.Text;
using System.Collections;
using System.Linq;

namespace UBER
{
    [Activity(Label = "UBER", MainLauncher = true)]
    public class MainActivity : Activity
    {

        static string uri = @"http://10.0.2.2:8080/api/Restaurants";
        public static Context contextt;
        private static List<Restaurant> rest = new List<Restaurant>();
        static ListView listRestaurants;

        private SearchView search;
        private static ArrayList ARestaurant;
        private ArrayAdapter<Restaurant> adatp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource 
            SetContentView(Resource.Layout.Main);

            listRestaurants = FindViewById<ListView>(Resource.Id.lstRests);
            //listRestaurants.ItemClick += ListRest_ItemClick;

            GettRestu restau = new GettRestu();
            restau.Execute();

            search = FindViewById<SearchView>(Resource.Id.searchView4);
            //search.SetQueryHint("Search for restuarant");

            listRestaurants.Adapter = new ProImageAdapter(this, rest);

            search.QueryTextChange += Search_QueryTextChange;

            search.QueryTextSubmit += (sender, e) => {
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
        }
        private void Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //adtp.Filter.InvokeFilter(e.NewText);
            //listProp.TextFilter(e.NewText);
        }

        //private void ListRest_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    Toast.MakeText(this, adatp.GetItem(e.Position).ToString(), ToastLength.Long).Show();
        //}
        /* Menu */
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MyMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.login:
                    var intent = new Intent(this, typeof(Login));
                    StartActivity(intent);
                    return true;
                case Resource.Id.register:
                    var intents = new Intent(this, typeof(Registration));
                    StartActivity(intents);
                    return true;
                case Resource.Id.restaurant:
                    var inten = new Intent(this, typeof(MainActivity));
                    StartActivity(inten);
                    return true;
                default:
                    return false;
            }
        }

        public class GettRestu : AsyncTask
        {
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                HttpClient client = new HttpClient();

                Uri url = new Uri(uri);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                var restaurant = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<Restaurant>>(restaurant);

                foreach (var g in result)
                {
                    rest.Add(g);
                }
                return true;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
                listRestaurants.Adapter = new ProImageAdapter(contextt, rest);
            }
        }

        public class ProImageAdapter : BaseAdapter<Restaurant>
        {
            private List<Restaurant> prope = new List<Restaurant>();
            static Context context;

            public ProImageAdapter(Context con, List<Restaurant> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;
                this.NotifyDataSetChanged();
            }

            public override Restaurant this[int position]
            {
                get
                {
                    return prope[position];
                }
            }

            public override int Count
            {
                get
                {
                    return prope.Count;
                }
            }
            public Context Mcontext
            {
                get;
                private set;
            }
            public override long GetItemId(int position)
            {
                return position;
            }

            public Bitmap getBitmap(byte[] getByte)
            {
                if (getByte.Length != 0)
                {
                    return BitmapFactory.DecodeByteArray(getByte, 0, getByte.Length);
                }
                else
                {
                    return null;
                }
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View restuarants = convertView;
                if (restuarants == null)
                {
                    restuarants = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Res_View, parent, false);
                }
                TextView txtName = restuarants.FindViewById<TextView>(Resource.Id.txtResName);
                TextView txtType = restuarants.FindViewById<TextView>(Resource.Id.txtResLocation);
                TextView txtDesc = restuarants.FindViewById<TextView>(Resource.Id.txtResCity);
                ImageView Image = restuarants.FindViewById<ImageView>(Resource.Id.Image);

                if (prope[position].Image != null)
                {
                    Image.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].Image, 0, prope[position].Image.Length));
                }

                txtName.Text = prope[position].Res_Name;
                txtType.Text = prope[position].Res_Location;
                txtDesc.Text = prope[position].Res_City;
                Image.Tag = prope[position].Image;
                return restuarants;
            }
        }

    }

}
