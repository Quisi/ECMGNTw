using EnergyScanVerwaltung;
using EnergyScanVerwaltung.DTOs;
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

namespace EnergyScanVerwaltung.AppPages
{
    /// <summary>
    /// Change Request Page with Values in Fields not Ids
    /// </summary>
    public partial class ChangeRequestApp : Page
    {
        public ObservableCollection<ChangeRequestDTO> ChangeRequests { get; } = new ObservableCollection<ChangeRequestDTO>();
        public ChangeRequestApp()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ChangeRequests.CollectionChanged += new NotifyCollectionChangedEventHandler(RefreshListview);
            Task<bool> task = GetChangeRequests();
        }

        private void BarcodeEditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BarcodeDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BarcodeAddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void RefreshListview(object sender, NotifyCollectionChangedEventArgs e)
        {
            Windows.UI.Xaml.Data.LoadMoreItemsResult reload = await XListview.LoadMoreItemsAsync();
        }
        private async Task<bool> GetChangeRequests()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/changeRequest"),
            };
            HttpResponseMessage response = await client.SendAsync(httpRequest);
            //string content = "{\"array\" : ";
            string content = await response.Content.ReadAsStringAsync();
            //content += "}";
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    List<ChangeRequestDTO> changeRequestDTOs = JsonConvert.DeserializeObject<List<ChangeRequestDTO>>(content);
                    foreach(ChangeRequestDTO dto in changeRequestDTOs)
                    {
                        ChangeRequests.Add(dto);
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

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
