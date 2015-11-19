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

namespace ChatClient.Droid
{
    [Activity(Label = "Meetup Chat", MainLauncher = true)]
    public class Login : Activity
    {
        #region Methods
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            var input = FindViewById<EditText>(Resource.Id.InputLogin);
            var button = FindViewById<Button>(Resource.Id.Login);

            button.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(input.Text))
                {
                    var dlgAlert = (new AlertDialog.Builder(this)).Create();
                    dlgAlert.SetMessage("User name is mandatory !");
                    dlgAlert.SetTitle("Alert");
                    dlgAlert.SetButton("OK", HandllerButton);
                    dlgAlert.Show();
                }
                else 
                {
                    var mainActivity = new Intent(this, typeof(MainActivity));
                    mainActivity.PutExtra("username", input.Text);
                    StartActivity(mainActivity);
                }
            };

        } 
        #endregion

        #region Handlers

        public void HandllerButton(object sender, DialogClickEventArgs e) { }

        #endregion
    }
}