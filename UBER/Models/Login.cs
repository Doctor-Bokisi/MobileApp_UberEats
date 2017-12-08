
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UBER.Models;

namespace UBER
{
    [Activity(Label = "Login", MainLauncher = false)]
    public class Login : Activity
    {
       
        EditText textE, textP;
        Button login;
       
  
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            login = FindViewById<Button>(Resource.Id.btnLogin);

            login.Click += login_Click;
           
        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                ser1 dt = new ser1();

              

                textE = FindViewById<EditText>(Resource.Id.txtemail);
                textP = FindViewById<EditText>(Resource.Id.txtpassword);


                 Clients cust = new Clients();

                    cust = dt.GetCusts(textE.Text,textP.Text);

            
                if (string.IsNullOrEmpty(textE.Text) && string.IsNullOrEmpty(textP.Text))
                {
                    Toast.MakeText(this, "Enter Email and Password", ToastLength.Short).Show();
                }

                else if(string.IsNullOrEmpty(cust.ToString()))
                {
                    Toast.MakeText(this, "Email or Password wrong  " + cust.Email, ToastLength.Short).Show();
                }

                else
         
                    if (textE.Text == cust.Firstname && textP.Text == cust.Lastname)
                    {
                        Toast.MakeText(this, "Successfully logged as  " + cust.Email, ToastLength.Short).Show();
                       Intent ti = new Intent(this, typeof(stallPage));
                        ti.PutExtra("email", cust.Firstname);
                        ti.PutExtra("password", cust.Lastname);
                        StartActivity(ti);

                    }


                else
                    Toast.MakeText(this, "User does not exist in our database , please register", ToastLength.Short).Show();


            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }

        }
    }
}
