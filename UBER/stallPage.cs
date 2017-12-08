using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;


namespace UBER
{
    [Activity(Label = "Home")]
    public class stallPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.stallPage);
            // Create your application here

        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MyMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            string em = Intent.GetStringExtra("email");
            string pss = Intent.GetStringExtra("password");

            switch (item.ItemId)
            {
                
                case Resource.Id.login:
                    var intent = new Intent(this, typeof(Update));
                    intent.PutExtra("email", em);
                    intent.PutExtra("password", pss);
                    StartActivity(intent);
                    return true;

                case Resource.Id.viewRes:
                    var inten = new Intent(this, typeof(The_MainActivity));
                    inten.PutExtra("email", em);
                    inten.PutExtra("password", pss);
                    StartActivity(inten);
                    return true;

                default:
                    return false;
            }
        }
    }
}
