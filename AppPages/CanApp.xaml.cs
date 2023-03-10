using EnergyScanVerwaltung;
using EnergyScanVerwaltung.ActionPages;
using EnergyScanVerwaltung.DTOs;
using EnergyScanVerwaltung.Models;
using Newtonsoft.Json;
//using Microsoft.EntityFrameworkCore;
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
    public partial class CanApp : Page
    {
        public ObservableCollection<CanDTO> Cans { get; } = new ObservableCollection<CanDTO>();
        public CanApp()
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
            Button b = sender as Button;
            CanDTO c = b.DataContext as CanDTO;
            Frame.Navigate(typeof(EditCan), c);
        }

        private void CanDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CanAddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PostCan));
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
            
            content += await response.Content.ReadAsStringAsync();
            
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    List<CanDTO> canDTOs = JsonConvert.DeserializeObject<List<CanDTO>>(content);
                    foreach(CanDTO dto in canDTOs)
                    {
                        Cans.Add(dto);
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
