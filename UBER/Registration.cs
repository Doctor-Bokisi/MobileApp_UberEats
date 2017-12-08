
using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UBER.Models;

namespace UBER
{
    [Activity(Label = "Registration")]
    public class Registration : Activity
    {
        static string url = "http://10.0.2.2:8080/api/Regist"; 
            EditText FirstName, LastName, mobile, email, Password;
            HttpClient client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Registration);

            //FirstName = FindViewById<EditText>(Resource.Id.txt);
            FirstName = FindViewById<EditText>(Resource.Id.txtfirstName);
            LastName = FindViewById<EditText>(Resource.Id.txtLastName);
            mobile = FindViewById<EditText>(Resource.Id.txtMobile);
            email = FindViewById<EditText>(Resource.Id.txtEmail);
            Password = FindViewById<EditText>(Resource.Id.txtPassword);

            Button Regist = FindViewById<Button>(Resource.Id.btnRegist);
            Regist.Click += button_Clicked;
           
             
        }

        private async void button_Clicked(object sender, EventArgs e)
        {
            try
            {
                client = new HttpClient();
                var user = new Clients()
                {
                    Firstname = FirstName.Text,
                    Lastname = LastName.Text,
                    Mobile = mobile.Text,
                    Email = email.Text,
                    password = Password.Text
                };
              

            

                var uri = new System.Uri(string.Format(url));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);


                if(String.IsNullOrEmpty(FirstName.Text) && String.IsNullOrEmpty(LastName.Text) && String.IsNullOrEmpty(mobile.Text)&& String.IsNullOrEmpty(email.Text)&& String.IsNullOrEmpty(Password.Text))
                {
                    Toast.MakeText(this, "Cannot Register empty Text box , Fill in the Text Box", ToastLength.Long).Show();
                }


                else
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        Clients custs = JsonConvert.DeserializeObject<Clients>(data);

                        FirstName.Text = "";
                        LastName.Text = "";
                        mobile.Text = "";
                        email.Text = "";
                        Password.Text = "";
                        Toast.MakeText(this, "Thank you for registering with UberEats", ToastLength.Long).Show();
                        Intent ip = new Intent(this, typeof(TempActivity));
                        StartActivity(ip);
                    }
                }
              
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }

        }
          
    }
}
