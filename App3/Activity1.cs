using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App3
{
    [Activity(Label = "Activity1")]
    public class Activity1 : Activity
    { //japierdole
        Button b1;
        MediaRecorder recorder;
        MediaPlayer player;
        Button start;
        Button stop;
        Button b2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);

            start = FindViewById<Button>(Resource.Id.start);
            stop = FindViewById<Button>(Resource.Id.stop);
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.3gpp";
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);

            b2.Click += delegate
             {
                 Intent nextActivity = new Intent(this, typeof(Activity2));
                 StartActivity(nextActivity);
             };
            b1.Click += delegate
              {
                  Intent prevActivity = new Intent(this, typeof(MainActivity));
                  StartActivity(prevActivity);
              };
          
            start.Click += delegate
            {
                stop.Enabled = !stop.Enabled;
                start.Enabled = !start.Enabled;
                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.Default);
                recorder.SetAudioEncoder(AudioEncoder.Default);
                recorder.SetOutputFile(path);
                recorder.Prepare();
                recorder.Start();
            };
            stop.Click += delegate
            {
                stop.Enabled = !stop.Enabled;
                recorder.Stop();
                recorder.Reset();
                player.SetDataSource(path);
                player.Prepare();
                player.Start();
            };
        }
        protected override void OnResume()
        {
            base.OnResume();
            recorder = new MediaRecorder();
            player = new MediaPlayer();
            player.Completion += (sender, e) =>
            {
                player.Reset();
                start.Enabled = !start.Enabled;
            };
        }
        protected override void OnPause()
        {
            base.OnPause();
            player.Release();
            recorder.Release();
            player.Dispose();
            recorder.Dispose();
            player = null;
            recorder = null;
        }
    }
}