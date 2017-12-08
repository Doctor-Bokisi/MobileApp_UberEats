
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using UBER.Models;

namespace UBER
{
    [Activity(Label = "Products")]
    public class Products : Activity
    {
        static string uri = @"http://10.0.2.2:8080/api/productss";
        public static Context contextt;
        private static List<Product> rest = new List<Product>();
        private static List<Product> newItem = new List<Product>();
        static ListView listView;
        static double interim = 0;
        static int quantity = 0;
        static Intent intent = new Intent();
        static Button btnCart;
        TextView txtProdName;
        TextView txtProdDesc;
        ImageView imm;



        static string prodprice, ProdName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource 
            // SetContentView(Resource.Layout.Search);Android.Views.InflateExceptio
            SetContentView(Resource.Layout.MCDonalds);

            listView = FindViewById<ListView>(Resource.Id.lstPro);

            GettRestu restau = new GettRestu();
            restau.Execute();


            txtProdName = FindViewById<TextView>(Resource.Id.txtProdName);
            txtProdDesc = FindViewById<TextView>(Resource.Id.txtProdPrice);
            imm = FindViewById<ImageView>(Resource.Id.imageView1);


            intent = new Intent(this, typeof(Cart));


        }

        static void addedToCart(double total, int quan)
        {
            double tot = total;
            int q = quan;

            intent.PutExtra("quantity",q.ToString());
            intent.PutExtra("total", tot.ToString());

        }


        private void Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //adtp.Filter.InvokeFilter(e.NewText);
            //listProp.TextFilter(e.NewText);
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MyMenu, menu);
            return base.OnCreateOptionsMenu(menu);
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
                var result = JsonConvert.DeserializeObject<List<Product>>(restaurant);

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

        public class ProImageAdapter : BaseAdapter<Product>
        {
            private List<Product> prope = new List<Product>();
            static Context context;
           

            public ProImageAdapter(Context con, List<Product> lstP)
            {
                prope.Clear();
                context = con;
                prope = lstP;

                this.NotifyDataSetChanged();
            }

            public override Product this[int position]
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
                View propertie = convertView;
                if (propertie == null)
                {
                    propertie = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Prod_ListView, parent, false);
                }
                TextView txtProdName = propertie.FindViewById<TextView>(Resource.Id.txtProdName);
                TextView txtProdDesc = propertie.FindViewById<TextView>(Resource.Id.txtProdPrice);
                ImageView imm = propertie.FindViewById<ImageView>(Resource.Id.imageView1);
                 btnCart = propertie.FindViewById<Button>(Resource.Id.btnCart);


                if (prope[position].Image != null)
                {
                    imm.SetImageBitmap(BitmapFactory.DecodeByteArray(prope[position].Image, 0, prope[position].Image.Length));
                }

                EventHandler BtnCart_Click = HandleEventHandler;

                btnCart.Click += BtnCart_Click; 

             
                void HandleEventHandler(object sender, EventArgs e)
                {
                    double Total = 0;
                  
                    List<Product> pd = new List<Product>();
                    pd = rest;

                    if (pd[position] == rest[position]) 
                    {
                        Total = Convert.ToDouble(pd[position].ProdpPrice);
                           

                            interim += Total; 
                            Intent ti = new Intent();
                            intent.PutExtra("ProdName",pd[position].ProdName);    
                            intent.PutExtra("ProdPrice", pd[position].ProdpPrice);
                      
                        quantity++; 
                        addedToCart(interim,quantity);
                            
                    }
                }



                ProdName = prope[position].ProdName;
                prodprice = prope[position].ProdpPrice;
                txtProdName.Text = ProdName;
                txtProdDesc.Text = " R " + prodprice;
                imm.Tag = prope[position].Image;

              
                return propertie;
            } 
          
           
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.viewRes:
                    string em = Intent.GetStringExtra("email");
                    string pss = Intent.GetStringExtra("password");

                    intent.PutExtra("email", em);
                    intent.PutExtra("password", pss);
                    StartActivity(intent);
                    return true;


                default:
                    return false;
            }
        }

    }
}
