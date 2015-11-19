using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace ChatClient.iOS
{
    public partial class LoginViewController : UIViewController
    {
        #region Fields

        private UIWindow _window;

        #endregion

        #region Constructor

        public LoginViewController()
            : base("LoginViewController", null)
        {

            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            var logoImage = new UIImageView(UIImage.FromBundle("signalr"));
            logoImage.Frame = new RectangleF(5, 50, 310, 180);

            var labeluserName = new UILabel
            {
                Text = "User name.:"
            };
            labeluserName.Frame = new RectangleF(10, 220, 100, 100);

            var input = new UITextField();
            input.Frame = new RectangleF(10, 280, 300, 30);
            input.BackgroundColor = UIColor.LightGray;
            input.ShouldReturn = delegate
            {
                input.ResignFirstResponder();
                return true;
            };

            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle("Login", UIControlState.Normal);
            button.Frame = new RectangleF(10, 320, 300, 30);
            button.TouchUpInside += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(input.Text))
                {
                    new UIAlertView("Alert", "User name is mandatory!", null, "OK", null).Show();
                }
                else
                {

                    this.NavigationController.PushViewController(new MyViewController(input.Text), true);
                }
            };

            View.AddSubview(logoImage);
            View.AddSubview(labeluserName);
            View.AddSubview(input);
            View.AddSubview(button);

        }

        #endregion

        #region Methods

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        #endregion
    }
}