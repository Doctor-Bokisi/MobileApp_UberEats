
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace UBER
{
    [Activity(Label = "Main Page", MainLauncher = true)]
    public class TempActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Button btn_register, btn_Login;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainPage);
            // Create your application here
            //SetContentView(Resource.Layout.Main);

            btn_register = FindViewById<Button>(Resource.Id.btnRegister);
            btn_register.Click += reg_Clicked;

            btn_Login = FindViewById<Button>(Resource.Id.btnLogin);
            btn_Login.Click += login_Clicked;
        }

        private void reg_Clicked(object sender, EventArgs e)
        {
            Intent ip = new Intent(this, typeof(Registration));
            StartActivity(ip);
        }
        private void login_Clicked(object sender, EventArgs e)
        {
            Intent ip = new Intent(this, typeof(Login));
            StartActivity(ip);
        }
    }
}


