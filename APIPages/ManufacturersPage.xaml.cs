using EnergyScanVerwaltung;
using EnergyScanVerwaltung.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ManufacturersPage : Page
    {
        public ObservableCollection<Manufacturer> Manufacturers { get; } = new ObservableCollection<Manufacturer>();
        public ManufacturersPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Manufacturers.CollectionChanged += new NotifyCollectionChangedEventHandler(RefreshListview);
            Task<bool> task = GetManufacturers();
        }
        private void ManufacturerEditButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender.GetType().Equals(typeof(Button)) ? sender as Button : null;
            if (b != null)
            {
                foreach (Manufacturer obj in Manufacturers)
                {
                    if (obj.id.Equals(b.Tag))
                    {
                        Frame.Navigate(typeof(EditDualField), obj);
                    }
                }
            }
        }

        private void ManufacturerDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ManufacturerAddButton_Click(object sender, RoutedEventArgs e)
        {
            addGroup.Visibility = addGroup.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void RefreshListview(object sender, NotifyCollectionChangedEventArgs e)
        {
            Windows.UI.Xaml.Data.LoadMoreItemsResult reload = await XListview.LoadMoreItemsAsync();
        }
        private async Task<bool> GetManufacturers()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/manufacturer"),
            };
            HttpResponseMessage response = await client.SendAsync(httpRequest);
            string content = "{\"array\" : ";
            content += await response.Content.ReadAsStringAsync();
            content += "}";
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    Dictionary<string, List<Dictionary<string, string>>> a = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(content);
                    List<Dictionary<string, string>> b = a.GetValueOrDefault("array");
                    for (int i = 0; i < b.Count; i++)
                    {
                        Dictionary<string, string> c = b[i];

                        Manufacturers.Add(new Manufacturer(c.GetValueOrDefault("id"), c.GetValueOrDefault("name")));

                    }
                    return true;
                } catch (Exception e)
                {
                    Console.WriteLine("jsonexception: " + e.Message);
                    return false;
                }
            } else
            {
                return false;
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            addGroup.Visibility = Visibility.Collapsed;
            Manufacturer s = new Manufacturer() { name = addName.Text, id = "" };
            string json = JsonConvert.SerializeObject(s);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            var response = await client.PostAsync(new Uri(Root.Config.Route["API:BaseUrl"] + "/manufacturer"), data);
            if (response.IsSuccessStatusCode)
            {
                await GetManufacturers();
            } else
            {
                showErrorBox("StatusCode: " + response.StatusCode + ", Reason: " + response.ReasonPhrase);
            }
        }
        private void showErrorBox(string error)
        {
            ErrorBoxMessage.Text = error;
            Errorbox.Visibility = Visibility.Visible;
        }
        private void hideErrorBox(object sender, RoutedEventArgs e)
        {
            ErrorBoxMessage.Text = "";
            Errorbox.Visibility = Visibility.Collapsed;
        }
    }
}
