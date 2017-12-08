
using System;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using UBER.Models;

namespace UBER
{
    [Activity(Label = "OrderActivity", MainLauncher = false)]
    public class OrderActivity : Activity
    {
        Clients cust = new Clients();
        ser1 sev = new ser1();

        EditText  Address;
        Button btnOrder;

        static string totalAmnt;
        static string quan;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Check);


            totalAmnt = Intent.GetStringExtra("total");
            quan = Intent.GetStringExtra("quantity");
            string email = Intent.GetStringExtra("email");
            string password = Intent.GetStringExtra("password");

            cust = sev.GetCusts(email, password);

            Address = FindViewById<EditText>(Resource.Id.txtDeliveryAddress);
            btnOrder = FindViewById<Button>(Resource.Id.btnOrder);


             
            btnOrder.Click += BtnOrder_Click;
        }


        async void BtnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                    //cust_id = cust.CustId,
                    //totalAmount = totalAmnt,
                    //quantity = quan,
                    //address = Address.Text

                HttpClient client = new HttpClient();
                var user = new Order()
                {
                    cust_id = cust.CustId,
                    totalAmount = totalAmnt,
                    quantity = quan,
                    address = Address.Text

                };


                string url = "http://10.0.2.2:8080/api/Order";

                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Order pay = JsonConvert.DeserializeObject<Order>(data);

                    Address.Text = "";

                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);  
                    AlertDialog alert = dialog.Create();  
                    alert.SetTitle(" Order ");  
                    alert.SetMessage("Order placed successfully ");  
                    alert.SetButton("OK", (c, ev) =>  
                   {  
                    // Ok button click task 
                        Intent ip = new Intent(this, typeof(TrackOrder));
                        StartActivity(ip);
                    });  
                    alert.Show();  
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.Main:
                    Intent it = new Intent(this, typeof(The_MainActivity));
                    return true;

                default:
                    return false;
            }
        }
    }
}
