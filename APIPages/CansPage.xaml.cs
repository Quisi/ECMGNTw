using EnergyScanVerwaltung;
using EnergyScanVerwaltung.ActionPages;
using EnergyScanVerwaltung.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public partial class CansPage : Page
    {
        public ObservableCollection<Can> Cans { get; } = new ObservableCollection<Can>();
        public CansPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Cans.CollectionChanged += new NotifyCollectionChangedEventHandler(RefreshListview);
            Task<bool> task = GetCans();
        }

        private void CanEditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CanDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CanAddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PostCan));
            //Frame.Navigate(typeof(CreateCan));
        }

        private async void RefreshListview(object sender, NotifyCollectionChangedEventArgs e)
        {
            Windows.UI.Xaml.Data.LoadMoreItemsResult reload = await XListview.LoadMoreItemsAsync();
        }
        private async Task<bool> GetCans()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/can"),
            };
            HttpResponseMessage response = await client.SendAsync(httpRequest);
            string content = "";
            //content += "{\"array\" : ";
            content += await response.Content.ReadAsStringAsync();
            //content += "}";
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    List<Can> jsonCans = JsonConvert.DeserializeObject<List<Can>>(content);
                    foreach(Can can in jsonCans)
                    {
                        Cans.Add(can);
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
    }
}
