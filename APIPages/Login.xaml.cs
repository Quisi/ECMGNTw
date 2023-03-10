using EnergyScanVerwaltung;
using EnergyScanVerwaltung.DTOs;
using EnergyScanVerwaltung.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        /* private Button SubmitButton { get; set; }
         private TextBox XName { get; set; }
         private TextBox Email { get; set; }
         private PasswordBox Password { get; set; }
         private PasswordBox Password2 { get; set; }
         private ProgressBar LoginProgressBar { get; set; }
         private TextBox Errorbox { get; set; }*/
        private ApplicationDataContainer localSettings;
        private AppConfig appConfig;
        public Login()
        {
            InitializeComponent();
            appConfig = new AppConfig();
            localSettings = ApplicationData.Current.LocalSettings;
            String localsavedusername = localSettings.Values["username"] as string;
            if (localsavedusername != null)
            {
                Username.Text = localsavedusername;
            }
        }

        private void LoginRegisterRadio_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null && SubmitButton != null)
            {
                string tag = rb.Tag.ToString();
                if (tag.Equals("login"))
                {
                    SubmitButton.Content = "Login";
                    SubmitButton.Click += Login_Click;
                    SubmitButton.Click -= Register_Click;
                    Email.Visibility = Visibility.Collapsed;

                } else
                {
                    SubmitButton.Content = "Register";
                    SubmitButton.Click -= Login_Click;
                    SubmitButton.Click += Register_Click;
                    Email.Visibility = Visibility.Visible;

                }
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginProgressBar.Visibility = Visibility.Visible;
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
            {
                try
                {
                    TokenDTO tokenDTO = await GetTokenDTO(Username.Text, Password.Password);
                    if (tokenDTO != null)
                    {
                        localSettings.Values["username"] = Username.Text;
                        localSettings.Values["token"] = tokenDTO.access_token;
                        Frame.Navigate(typeof(Root), tokenDTO);
                    } else
                    {
                        Errorbox.Text = "Invalid Username/Password";
                        Errorbox.Visibility = Visibility.Visible;
                    }
                }catch(Exception eex)
                {
                    CancelCommandButton_Click(sender, e);
                }
            } else
            {
                Console.WriteLine("Error in Token retrieve methods...");
                Errorbox.Text = "Invalid Username/Password";
                Errorbox.Visibility = Visibility.Visible;
            }
            LoginProgressBar.Visibility = Visibility.Collapsed;
        }
        private async void CancelCommandButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("Connection to the Server could not be established. Invalid config?");

            // Add command and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoginProgressBar.Visibility = Visibility.Visible;
            string name = Username.Text;
            string password = Password.Password;
            string email = Email.Text;
            //TODO: Check Input, Register User, Send Email
            LoginProgressBar.Visibility = Visibility.Collapsed;
        }
        private async Task<TokenDTO> GetTokenDTO(string username, string password)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(appConfig.Route["API:BaseUrl"]+"/authorize?username=" + username + "&password=" + password)
            };
            HttpResponseMessage response = await client.SendAsync(httpRequest);
            string content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    TokenDTO tokenDTO  = JsonConvert.DeserializeObject<TokenDTO>(content);
                    return tokenDTO;
                    //Dictionary<string, string> a = JsonConvert.DeserializeObject<Dictionary<String, String>>(content);
                    //return a.GetValueOrDefault("access_token");
                } catch (Exception e)
                {
                    Console.WriteLine("jsonexception: " + e.Message);
                    return null;
                }
            } else
            {
                return null;
            }
        }
    }
}
