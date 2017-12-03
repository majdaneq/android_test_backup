using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Util;
using Android.Widget;
using Android.Runtime;
using Android.Content;

namespace App3
{

    

    [Activity(Label = "Activity2")]
    public class Activity2 : Activity, ILocationListener
    {
        Button b61;
        
        static readonly string TAG = "X:" + typeof(Activity2).Name;
        TextView _addressText;
        Location _currentLocation;
        LocationManager _locationManager;

        string _locationProvider;
        TextView _locationText;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout2);
            b61 = FindViewById<Button>(Resource.Id.buttonxxx);
            b61.Click += delegate
            {
                Intent nextActivity2 = new Intent(this, typeof(Activity3));
                StartActivity(nextActivity2);
            };
            _addressText = FindViewById<TextView>(Resource.Id.address_text);
            _locationText = FindViewById<TextView>(Resource.Id.location_text);
            FindViewById<TextView>(Resource.Id.get_address_button).Click += AddressButton_OnClick;

            InitializeLocationManager();
            // Create your application here
        }


        void InitializeLocationManager()
        {

            //The LocationManager class will listen for GPS updates from the device and notify the application by way of events.In this example we ask Android for the best location provider that matches a given set of Criteria and provide that provider to LocationManager.
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                _locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                _locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
                Address address = await ReverseGeocodeCurrentLocation();
                DisplayAddress(address);
            }
        }


        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }



        protected override void OnResume()
        { //Override OnResume so that Activity1 will begin listening to the LocationManager when the activity comes into the foreground:
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        { //Override OnPause and unsubscribe Activity1 from the LocationManager when the activity goes into the background:
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (_currentLocation == null)
            {
                _addressText.Text = "Can't determine the current address. Try again in a few minutes.";
                return;
            }

            Address address = await ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }

        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                _addressText.Text = deviceAddress.ToString();
            }
            else
            {
                _addressText.Text = "Unable to determine the address. Try again in a few minutes.";
            }
        }


    }
}