using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using ChatClient.Shared;

namespace ChatClient.Droid
{
    [Activity(Label = "Meetup Chat", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var userName = Intent.GetStringExtra("username");

            var client = new Client(string.Format("Android: {0}", userName));

            var input    = FindViewById<EditText>(Resource.Id.Input);
            var button   = FindViewById<Button>(Resource.Id.Send);
            var messages = FindViewById<ListView>(Resource.Id.Messages);

            var inputManager = (InputMethodManager)GetSystemService(InputMethodService);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());

            messages.Adapter = adapter;

            client.OnMessageReceived +=
             (sender, message) => RunOnUiThread(() =>
               adapter.Add(message));

            await client.Connect();

            input.EditorAction +=
              delegate
              {
                  inputManager.HideSoftInputFromWindow(input.WindowToken, HideSoftInputFlags.None);
              };

            button.Click += (s, e) => 
            {
                if (string.IsNullOrEmpty(input.Text))
                    return;

                client.Send(input.Text);
                input.Text = "";
            };
        }
    }
}
