using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ChatClient.WinPhone.Resources;
using ChatClient.Shared;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatClient.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Fields

        private string _userName;
        private ObservableCollection<string> _messages;
        private bool _isRunning;

        #endregion

        #region Constructor
        public MainPage()
        {
            InitializeComponent();
            _messages = new ObservableCollection<string>();
            DataContext = _messages;
        }
        
        #endregion

        #region Methods


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (!_isRunning)
            {
                _isRunning = true;
                _userName = NavigationContext.QueryString["userName"] as string;

                var client = new Client(string.Format("WinPhone: {0}", _userName));

                client.OnMessageReceived +=
                (sender, message) => Dispatcher.BeginInvoke(() =>
                {
                    _messages.Add(message);
                });

                Task.Run(async () =>
                {
                    await client.Connect();
                });

                btnSend.Click += (s, args) =>
                {
                    if (string.IsNullOrEmpty(txtMessage.Text))
                        return;

                    client.Send(txtMessage.Text);
                    txtMessage.Text = "";
                };
            }
        }
        
        #endregion

        #region Handlers
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
             
        } 
        #endregion
    }
}