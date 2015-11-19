using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ChatClient.WinPhone
{
    public partial class Login : PhoneApplicationPage
    {
        #region Constructor
        public Login()
        {
            InitializeComponent();
        } 
       
        #endregion

        #region Handlers

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
                MessageBox.Show("Alert", "User name is mandatory!", MessageBoxButton.OK);
            else
                NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?userName={0}", txtUserName.Text), UriKind.Relative));
        } 
        #endregion
    }
}