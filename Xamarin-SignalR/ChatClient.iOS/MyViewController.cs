using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace ChatClient.iOS
{
    public class MyViewController : DialogViewController
    {
        #region Fields

        private readonly EntryElement _input;
        private readonly Section _messages;
        private readonly Client _client;

        #endregion

        #region Constructor

        public MyViewController(string userName)
            : base(UITableViewStyle.Grouped, null)
        {
            _input = new EntryElement(null, "Enter message", null)
            {
                ReturnKeyType = UIReturnKeyType.Send
            };
            _messages = new Section();

            Root = new RootElement("Meetup Chat")
                       {
                           _messages,
                           new Section {_input}
                       };

            _client = new Client(string.Format("iOS: {0}", userName));
        }

        #endregion

        #region Methods
        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            _client.OnMessageReceived +=
               (sender, message) => InvokeOnMainThread(
                   () => _messages.Add(new StringElement(message)));

            await _client.Connect();

            _input.ShouldReturn += () =>
            {
                _input.ResignFirstResponder(true);

                if (string.IsNullOrEmpty(_input.Value))
                    return true;

                _client.Send(_input.Value);
                _input.Value = "";

                return true;
            };
        }
    }
        #endregion
}

