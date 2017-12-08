
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UBER.Models;
//using Android.Views;
using static UBER.Products;

namespace UBER
{
    [Activity(Label = "Cart")]
    public class Cart : Activity
    {
  
        static string TotalA = "";
        static string Quan = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CartList);

            Intent intent = new Intent();
            ListView listView2;
            TextView txt , txtQuan , txtP;
            Button btncheckOut;
       
            List<string> Mylist = new List<string>();


            listView2 = FindViewById<ListView>(Resource.Id.CartLST);
            txt = FindViewById<TextView>(Resource.Id.xartTotal);
            txtP = FindViewById<TextView>(Resource.Id.xartPname);
            txtQuan = FindViewById<TextView>(Resource.Id.xartQuantity);
            btncheckOut = FindViewById<Button>(Resource.Id.btnCheckout);


            ISharedPreferences pref = Application.Context.GetSharedPreferences("Cart", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            string pname = null;
            string pPrice = null;


            pname = Intent.GetStringExtra("ProdName");
            pPrice = Intent.GetStringExtra("ProdPrice");
             string Total = Intent.GetStringExtra("total");
            string quantiy = Intent.GetStringExtra("qunatity");

            TotalA = Total;
            Quan = quantiy;

            txt.Text = "Total Amount : " + "R " + TotalA;
            txtP.Text = pname;
            txtQuan.Text = "Number of Quantity : " + Quan;

            Mylist.Add("Item name : " + pname);
            //Mylist.Add(pPrice);
            Mylist.Add(txt.Text);
            Mylist.Add(txtQuan.Text);

          

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, Mylist);
        
            listView2.Adapter = adapter;  
            btncheckOut.Click += BtncheckOut_Click;

        }

        void BtncheckOut_Click(object sender, EventArgs e)
        {
            Intent ip = new Intent(this, typeof(PaymentActivity));
            //ip.PutExtra("total",);
            string customer_id = Intent.GetStringExtra("cust_Id");
            string first_name = Intent.GetStringExtra("name");
            string email = Intent.GetStringExtra("email");
            string password = Intent.GetStringExtra("password");
            ip.PutExtra("cust_Id", customer_id);
            ip.PutExtra("name", first_name);
            ip.PutExtra("Total",TotalA);
            ip.PutExtra("quantity", Quan);
            ip.PutExtra("email", email);
            ip.PutExtra("password", password);
            StartActivity(ip);
        }
    }
}
