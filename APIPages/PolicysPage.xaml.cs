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
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PolicysPage : Page
    {
        public ObservableCollection<Policy> Policys { get; } = new ObservableCollection<Policy>();
        public PolicysPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Policys.CollectionChanged += new NotifyCollectionChangedEventHandler(RefreshListview);
            Task<bool> task = GetPolicys();
        }
        private void PolicyEditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PolicyDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PolicyAddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void RefreshListview(object sender, NotifyCollectionChangedEventArgs e)
        {
            Windows.UI.Xaml.Data.LoadMoreItemsResult reload = await XListview.LoadMoreItemsAsync();
        }
        private async Task<bool> GetPolicys()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/policy"),
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

                        Policys.Add(new Policy(c.GetValueOrDefault("id"), c.GetValueOrDefault("name")));

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
