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
using Android.Provider;
using Android.Graphics;

namespace App3
{
    [Activity(Label = "Activity4")]
    public class Activity4 : Activity
    {

        int count = 1;
        ImageView image;
        Button b1;
        Button b2;
        Button b3;

        protected override void OnCreate(Bundle savedInstanceState)
        {            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout4);
            b1 = FindViewById<Button>(Resource.Id.myButton);
            b2 = FindViewById<Button>(Resource.Id.button1);
            b3 = FindViewById<Button>(Resource.Id.button2);
            image = FindViewById<ImageView>(Resource.Id.imageView1);
            b1.Click += Camera_Click;
            b2.Click += delegate
            {
                Intent prevAct = new Intent(this, typeof(Activity3));
                StartActivity(prevAct);
            };
            b3.Click += delegate 
            {
                Intent nAct = new Intent(this, typeof(Activity5));
                StartActivity(nAct);
            };
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            image.SetImageBitmap(bitmap);
        }
    
        private void Camera_Click(object sender,EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }


}


