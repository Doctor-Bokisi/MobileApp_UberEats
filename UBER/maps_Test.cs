
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UBER
{
    [Activity(Label = "maps_Test", MainLauncher = false)]
    public class maps_Test : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var geoUri = Android.Net.Uri.Parse("geo: -26.102892,28.028855099999987");
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            StartActivity(mapIntent);
        }
    }
}
