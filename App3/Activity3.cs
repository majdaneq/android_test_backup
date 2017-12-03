using System;  
using Android.App;  
using Android.Content;  
using Android.Runtime;  
using Android.Views;  
using Android.Widget;  
using Android.OS;  
using Android.Locations;  
using System.Collections.Generic;  
using Android.Util;  
using System.Linq;  
namespace App3
{
    [Activity(Label = "CurrentLocation")]
    public class Activity3 : Activity, ILocationListener
    {
        Button b1;
        Button b2;
        TextView txtlatitu;
        TextView txtlong;
        Location currentLocation;
        LocationManager locationManager;
        string locationProvider;


        public string TAG
        {
            get;
            private set;
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.layout3);
            txtlatitu = FindViewById<TextView>(Resource.Id.txtlatitude);
            txtlong = FindViewById<TextView>(Resource.Id.txtlong);
            InitializeLocationManager();
            b1 = FindViewById<Button>(Resource.Id.button1);
            b2 = FindViewById<Button>(Resource.Id.button2);

            b1.Click += delegate
              {
                  Intent nextActivity2 = new Intent(this, typeof(Activity4));
                  StartActivity(nextActivity2);
              };

            b2.Click += delegate
              {
                  Intent prevActivity2 = new Intent(this, typeof(Activity3));
                  StartActivity(prevActivity2);
              };
        }

        private void InitializeLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + locationProvider + ".");
        }

        protected override void OnResume()
        {
            base.OnResume();
            locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            currentLocation = location;
            if (currentLocation == null)
            {
                //Error Message  
            }
            else
            {
                txtlatitu.Text = currentLocation.Latitude.ToString();
                txtlong.Text = currentLocation.Longitude.ToString();
            }
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }

}