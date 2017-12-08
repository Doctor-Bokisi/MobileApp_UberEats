
using System.Timers;
using Android.App;
using Android.OS;
using Android.Widget;
using RadialProgress;

namespace UBER
{
    [Activity(Label = "TrackOrder",MainLauncher = true)]
    public class TrackOrder : Activity
    {

        RadialProgressView rad;
        TextView txtTimer;
        int hours = 0;
        int min = 0;
        int sec = 0;
        Timer timer;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AnimatedCar);

            rad = FindViewById<RadialProgressView>(Resource.Id.radialProgress);
            txtTimer = FindViewById<TextView>(Resource.Id.Timer);

        

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

         
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sec++;

            if(sec == 60)
            {
                min++;
                sec = 0;
            }
            if (min == 60)
            {
                hours++;
                min = 0;
            }

            RunOnUiThread(() => { txtTimer.Text = $"{hours} : {min} : {sec}"; });
            rad.Value = sec;
        }
    }
}
