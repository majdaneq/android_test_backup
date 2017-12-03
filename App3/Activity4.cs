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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout4);

            b1 = FindViewById<Button>(Resource.Id.myButton);
            image = FindViewById<ImageView>(Resource.Id.imageView1);

            b1.Click += Camera_Click;
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


