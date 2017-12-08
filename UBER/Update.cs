
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UBER.Models;

namespace UBER
{
    [Activity(Label = "Update")]
    public class Update : Activity
    {

        Button profile;
        EditText txtFirst, txtLast, txtMobile,txtId,textE,textP;
        Clients cust = new Clients();
        ser1 dt = new ser1();
   
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Update);
           
            var loggedOn = Application.Context.GetSharedPreferences("CustomerList", FileCreationMode.Private);
           
            string email = Intent.GetStringExtra("email");
            string password = Intent.GetStringExtra("password");
            cust = dt.GetCusts(email, password);


            txtFirst = FindViewById<EditText>(Resource.Id.editFirstname);
            txtLast = FindViewById<EditText>(Resource.Id.editLastName);
            txtMobile = FindViewById<EditText>(Resource.Id.editMobile);
            textE = FindViewById<EditText>(Resource.Id.txtemail);
            textP = FindViewById<EditText>(Resource.Id.txtpassword);

            txtFirst.Text = cust.Email;
            txtLast.Text = cust.Mobile;
            txtMobile.Text = "0786765454";

            profile = FindViewById<Button>(Resource.Id.btnUpdate);

            profile.Click += BtnUpdate_profile;

        }

        private void BtnUpdate_profile(object sender, EventArgs e)
        {
            
            try
            { 
                cust.Firstname = txtFirst.Text;
                cust.Lastname = txtLast.Text;
                cust.Mobile = txtMobile.Text;

                dt.Update(cust,cust.CustId);

                Toast.MakeText(this, "User information updated successfully", ToastLength.Short).Show();
                Intent inter = new Intent(this, typeof(The_MainActivity));
                StartActivity(inter);
            }
            catch (Exception error)
            {
                Toast.MakeText(this, error.ToString(), ToastLength.Short).Show();
                Intent inters = new Intent(this, typeof(The_MainActivity));
                inters.PutExtra("cust_Id",cust.CustId);
                inters.PutExtra("name",cust.Email);
                StartActivity(inters);

            }

        }
    }
}
