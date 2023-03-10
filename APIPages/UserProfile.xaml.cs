using EnergyScanVerwaltung;
using EnergyScanVerwaltung.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// User profile page
    /// </summary>
    public sealed partial class UserProfile : Page
    {
        public UserProfile() 
        {
            InitializeComponent();       
            Username.Text = Root.tokenDTO.Username;
            Email.Text = Root.tokenDTO.Email;
            Verified.IsChecked = Root.tokenDTO.Verified;
            Verified.IsEnabled = false;
            if (Root.tokenDTO.Group != null)
            {
                Group.Text = Root.tokenDTO.Group.Name;
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Root.tokenDTO = null;
            App.instance.rootFrame.Navigate(typeof(Login));
        }
    }
}
