
using Android.App;
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

namespace UBER
{
    [Activity(Label = "View Restaurant", MainLauncher = false ,Theme = "@android:style/Theme.Holo")]
    public class The_MainActivity : Activity
    {
        static string uri = @"http://10.0.2.2:8080/api/Restaurants";
        public static Context contextt;
        private static List<Restaurant> rest = new List<Restaurant>();
        static ListView listView;

      // private SearchView search;
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource 
           // SetContentView(Resource.Layout.Search);
            SetContentView(Resource.Layout.Search);

            listView = FindViewById<ListView>(Resource.Id.lstRests);
            GettRestu restau = new GettRestu();
            restau.Execute();

           // search = FindViewById<SearchView>(Resource.Id.searchView4);
       
            listView.ItemClick += ListView_ItemClick;  
        }

        void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent ti = new Intent(this, typeof(Products));
            string email = Intent.GetStringExtra("email");
            string password = Intent.GetStringExtra("password");
            ti.PutExtra("email", email);
            ti.PutExtra("password", password);

            StartActivity(ti);
        }

        private void Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //adtp.Filter.InvokeFilter(e.NewText);
            //listProp.TextFilter(e.NewText);
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
                listView.Adapter = new ProImageAdapter(contextt, rest);
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
