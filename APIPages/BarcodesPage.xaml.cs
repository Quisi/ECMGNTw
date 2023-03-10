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

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class BarcodesPage : Page
    {
        public ObservableCollection<Barcode> Barcodes { get; } = new ObservableCollection<Barcode>();
        public BarcodesPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Barcodes.CollectionChanged += new NotifyCollectionChangedEventHandler(RefreshListview);
            Task<bool> task = GetBarcodes();
        }

        private void BarcodeEditButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender.GetType().Equals(typeof(Button)) ? sender as Button : null;
            if (b != null)
            {
                foreach (Barcode obj in Barcodes)
                {
                    if (obj.id.Equals(b.Tag))
                    {
                        Frame.Navigate(typeof(EditDualField), obj);
                    }
                }
            }
        }

        private void BarcodeDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BarcodeAddButton_Click(object sender, RoutedEventArgs e)
        {
            addGroup.Visibility = addGroup.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void RefreshListview(object sender, NotifyCollectionChangedEventArgs e)
        {
            Windows.UI.Xaml.Data.LoadMoreItemsResult reload = await XListview.LoadMoreItemsAsync();
        }
        private async Task<bool> GetBarcodes()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/barcode"),
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

                        Barcodes.Add(new Barcode() { id = c.GetValueOrDefault("id"), name =c.GetValueOrDefault("name") });

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
            Barcode s = new Barcode() { name = addName.Text, id = "" };
            string json = JsonConvert.SerializeObject(s);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            var response = await client.PostAsync(new Uri(Root.Config.Route["API:BaseUrl"] + "/barcode"), data);
            if (response.IsSuccessStatusCode)
            {
                await GetBarcodes();
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
