using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using System;
using System.Linq;
using Xamarin.Essentials;

namespace MarqueeSwap
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindViewById<Android.Widget.EditText>(Resource.Id.editCurrent).Text = Preferences.Get("current_text", "Space Jam\nFri 7pm\nWatch out for our kids");
            FindViewById<Android.Widget.EditText>(Resource.Id.editWanted).Text = Preferences.Get("wanted_text", "Shang Chi\nFri 6pm\nOh No Not again");

            FindViewById<Android.Widget.Button>(Resource.Id.buttonSwap).Click += MainActivity_Click;
        }

        static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        private void MainActivity_Click(object sender, System.EventArgs e)
        {
            string CurrentText = FindViewById<Android.Widget.EditText>(Resource.Id.editCurrent).Text.ToUpper().Replace(" ", "").Replace("\n", "");
            string addText = FindViewById<Android.Widget.EditText>(Resource.Id.editWanted).Text.ToUpper().Replace(" ", "").Replace("\n", "");
            string removeText = "";

            Preferences.Set("current_text", FindViewById<Android.Widget.EditText>(Resource.Id.editCurrent).Text);
            Preferences.Set("wanted_text", FindViewById<Android.Widget.EditText>(Resource.Id.editWanted).Text);

            foreach (char c in CurrentText)
            {
                int indexOfChar = addText.IndexOf(c);
                if (indexOfChar > -1)
                {
                    addText = addText.Remove(indexOfChar,1);
                }
                else
                {
                    removeText += c;
                }
            }

            FindViewById<Android.Widget.TextView>(Resource.Id.textRemove).Text = SortString(removeText);
            FindViewById<Android.Widget.TextView>(Resource.Id.textAdd).Text = SortString(addText);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}