
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
    [Activity(Label = "PaymentActivity")]
    public class PaymentActivity : Activity
    {

        EditText CardName, CardNumber, cvv , custId;
        Button payment;
        static string email = null;
        static string password = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PayP);

            CardName = FindViewById<EditText>(Resource.Id.txtCarName);
            CardNumber = FindViewById<EditText>(Resource.Id.txtCardNumber);
            cvv = FindViewById<EditText>(Resource.Id.txtCvv);
            payment = FindViewById<Button>(Resource.Id.btnPay);
            custId = FindViewById<EditText>(Resource.Id.txtID);


            email  = Intent.GetStringExtra("email");
            password= Intent.GetStringExtra("password");

            ser1 sv = new ser1();

            Clients ct = sv.GetCusts(email, password);

            custId.Text = ct.CustId.ToString();

            payment.Click += Payment_Click;
        }



        async void  Payment_Click(object sender, EventArgs e)
        {
           try
            {
                HttpClient client = new HttpClient();

                var user = new Payment()
                {
                    CardName = CardName.Text,
                    CardNumber = CardNumber.Text,
                    Cvv = cvv.Text,
                    cust_Id = Convert.ToInt32(custId.Text)
                };

                CardName.Text = "";
                CardNumber.Text = "";
                cvv.Text = "";
                custId.Text = "";

                string url = "http://10.0.2.2:8080/api/pay"; 
                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

               

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Payment pay = JsonConvert.DeserializeObject<Payment>(data);


                     Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);  
                    AlertDialog alert = dialog.Create();  
                    alert.SetTitle("Checkout ");  
                    alert.SetMessage("Payment was successful ");  
                    alert.SetButton("OK", (c, ev) =>  
                   {  

                        Intent ip = new Intent(this, typeof(OrderActivity));
                        string Total = Intent.GetStringExtra("total");
                       string quan = Intent.GetStringExtra("quantity");

                       ip.PutExtra("total", Total);
                       ip.PutExtra("quantity", quan);
                       ip.PutExtra("email", email);
                       ip.PutExtra("password", password);
                       StartActivity(ip);
                    // Ok button click task  
                    });  
                    alert.Show();  

                }


            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
       
              
        }
    }
}
