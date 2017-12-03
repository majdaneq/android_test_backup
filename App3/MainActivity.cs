using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Media;
using Android.Content;
using Android;

namespace App3
{
    [Activity(Label = "App3", MainLauncher = true)]
    public class MainActivity : Activity
    {
#region initialize
        int countb = 1;
        int countt = 0;
        int countm = 0;
        string pass = "";
        string passcorrect = "2137";
        Button button;
        TextView text;
        CheckBox check;
        TextView text2;
        RadioButton radio;
        SeekBar sznup;
        TextView text3;
        TextView text4;
        EditText pw;
        TextView textpw;
        Button b2;
        ImageView obrazek;
        MediaPlayer _player;
#endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {   
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.Main);
            // Set our view from the "main" layout resource
#region deklaracje
            button = FindViewById<Button>(Resource.Id.b1);
            check = FindViewById<CheckBox>(Resource.Id.checkBox1);
            text = FindViewById<TextView>(Resource.Id.textView1);
            text2 = FindViewById<TextView>(Resource.Id.TextViewx);
            radio = FindViewById<RadioButton>(Resource.Id.radioButton1);
            sznup = FindViewById<SeekBar>(Resource.Id.seekBar1);
            text3 = FindViewById<TextView>(Resource.Id.textView2);
            text4 = FindViewById<TextView>(Resource.Id.textView3);
            pw = FindViewById<EditText>(Resource.Id.editText1);
            textpw = FindViewById<TextView>(Resource.Id.TextViewpw);
            b2 = FindViewById<Button>(Resource.Id.button1);
            obrazek = FindViewById<ImageView>(Resource.Id.imageView1);
            _player = MediaPlayer.Create(this, Resource.Drawable.testsound);
            #endregion


            #region obsluga
            b2.Click += delegate
            {
                if (pass.Length == 4 && pass == passcorrect)
                {
                    textpw.Text = string.Format("Poprawne hasło!");
                    text2.Text = pass;
                    b2.Activated = false;
                    pw.Activated = false;
                    obrazek.SetImageResource(Resource.Drawable.image);
                    _player.Start();
                    Intent nextActivity = new Intent(this, typeof(Activity1));
                    StartActivity(nextActivity);
                }
                if (pass.Length != 4)
                {
                    textpw.Text = string.Format("wprowadź całe hasło jełopie");                    
                }

                if (pass.Length == 4 && pass != passcorrect)
                {
                    textpw.Text = string.Format("chujowe hasło");
                }
            };

            button.Click += delegate
            {
                Message(button);          //text.Text = string.Format("{0} clicks", count++); 
            };

            pw.TextChanged += delegate
            {
                b2.Activated = true;
                pass = pw.Text;
            };

            check.Click += delegate
            {
                if (check.Checked)
                {
                    text2.Text = string.Format("Checked");
                }
                else
                {
                    text2.Text = string.Format("Not checked");
                }
            };

            radio.Click += delegate
            {
                if (radio.Checked)
                {
                    Radio(radio);
                }
                else
                    text3.Text = "";
            };

            sznup.ProgressChanged += delegate
            {
                slide(sznup);
            };
        }
        #endregion


        #region funkcje_obslug
        public void Message(object sender)
        {
            text2.Text = string.Format("jp2gmd");
            button.Text = string.Format("{0} clicks", countb++);
            text.Text = string.Format("{0} hehehe", countt++);
            Toast.MakeText(this, "You clicked" + countm + "times", ToastLength.Long).Show();
            countm++;
        }

        public void checkCheck(object sender)
        {
            text2.Text = string.Format("jp3gmd");
        }

        public void Radio(object sender)
        {
            text3.Text = string.Format("dzialam");
        }

        public void slide(object sender)
        {
            text4.Text = string.Format("JP NA {0} procent", sznup.Progress);
        }

#endregion

    }

}

