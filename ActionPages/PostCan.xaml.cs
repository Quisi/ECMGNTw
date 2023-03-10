using EnergyScanVerwaltung.DTOs;
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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace EnergyScanVerwaltung.ActionPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PostCan : Page
    {
        CanDTO CanDTO;
        BarcodeDTO BarcodeDTO;
        ManufacturerDTO ManufacturerDTO;
        TasteDTO TasteDTO;
        CountryDTO CountryDTO;
        StatusDTO StatusDTO;

        ObservableCollection<ManufacturerDTO> manufacturers;
        
        ObservableCollection<TasteDTO> tastes;
        ObservableCollection<CountryDTO> countries;

        public PostCan()
        {
            this.InitializeComponent();
            CanDTO = new CanDTO() { Id = "1" };
            BarcodeDTO = new BarcodeDTO() { Id = "1" };
            ManufacturerDTO = new ManufacturerDTO() { Id = "1" };
            TasteDTO = new TasteDTO() { Id = "1" };
            CountryDTO = new CountryDTO() { Id = "1" };
            StatusDTO = new StatusDTO() { Id = "1", Name = "new" };
            manufacturers = new ObservableCollection<ManufacturerDTO>();
            tastes = new ObservableCollection<TasteDTO>();
            countries = new ObservableCollection<CountryDTO>();
            //manufacturers.CollectionChanged += new NotifyCollectionChangedEventHandler(RefreshListview);
            GetComboData();
            
        }
        private async void RefreshListview(object sender, NotifyCollectionChangedEventArgs e)
        {
            //manufacturercobobox.UpdateLayout();
        }

        private async void GetComboData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/manufacturer"),
            };
            HttpResponseMessage response = await client.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    manufacturers = JsonConvert.DeserializeObject< ObservableCollection < ManufacturerDTO >>(content);
                } catch (Exception e)
                {
                    Console.WriteLine("jsonexception: " + e.Message);
                }
            }
            manufacturers.Add(new ManufacturerDTO() { Id = "-1", Name = "New" });
            manufacturercobobox.ItemsSource = manufacturers;
            httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/taste"),
            };
            response = await client.SendAsync(httpRequest);
            content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    tastes = JsonConvert.DeserializeObject<ObservableCollection<TasteDTO>>(content);
                } catch (Exception e)
                {
                    Console.WriteLine("jsonexception: " + e.Message);
                }
            }
            httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/country"),
            };
            response = await client.SendAsync(httpRequest);
            content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                try
                {
                    countries = JsonConvert.DeserializeObject<ObservableCollection<CountryDTO>>(content);
                } catch (Exception e)
                {
                    Console.WriteLine("jsonexception: " + e.Message);
                }
            }
            tastes.Add(new TasteDTO() { Id = "-1", Name = "New" });
            tastescobobox.ItemsSource = tastes;
            countries.Add(new CountryDTO() { Id = "-1", Name = "New" });
            countriescobobox.ItemsSource = countries;
        }

        private async void Create_ClickAsync(object sender, RoutedEventArgs e)
        {
            bool inputvalidationerror = false;
            if(ManufacturerDTO == null || string.IsNullOrWhiteSpace(ManufacturerDTO.Name))
            {
                if(string.IsNullOrWhiteSpace(manuf.Text))
                {
                    manufacturercobobox.BorderBrush = new SolidColorBrush(Colors.Red);
                    manufacturercobobox.BorderThickness = new Thickness(1);
                    inputvalidationerror = true;
                }
                else
                {
                    manufacturercobobox.BorderBrush = null;
                    ManufacturerDTO = new ManufacturerDTO() { Id = "1", Name = manuf.Text };
                }
            }
            if (TasteDTO == null || string.IsNullOrWhiteSpace(TasteDTO.Name))
            {
                if (string.IsNullOrWhiteSpace(taste.Text))
                {
                    tastescobobox.BorderBrush = new SolidColorBrush(Colors.Red);
                    tastescobobox.BorderThickness = new Thickness(1);
                    inputvalidationerror = true;
                } else
                {
                    tastescobobox.BorderBrush = null;
                    TasteDTO = new TasteDTO() { Id = "1", Name = taste.Text };
                }
            }
            if (CountryDTO == null || string.IsNullOrWhiteSpace(CountryDTO.Name))
            {
                if (string.IsNullOrWhiteSpace(coun.Text))
                {
                    countriescobobox.BorderBrush = new SolidColorBrush(Colors.Red);
                    countriescobobox.BorderThickness = new Thickness(1);
                    inputvalidationerror = true;
                } else
                {
                    countriescobobox.BorderBrush = null;
                    CountryDTO = new CountryDTO() { Id = "1", Name = coun.Text };
                }
            }
            if (string.IsNullOrWhiteSpace(BarcodeDTO.Name))
            {
                barcode.BorderBrush = new SolidColorBrush(Colors.Red);
                barcode.BorderThickness = new Thickness(1);
                inputvalidationerror = true;
            }
                
            if(inputvalidationerror)
            { return; }
            CanDTO.Barcodes = new List<BarcodeDTO>() { BarcodeDTO };
            CanDTO.Manufacturer = ManufacturerDTO;
            CanDTO.Taste = TasteDTO;
            CanDTO.Country = CountryDTO;
            CanDTO.Status = StatusDTO;
            CanDTO.Id = "1";
            string json = JsonConvert.SerializeObject(CanDTO);
            Uri uri = new Uri(Root.Config.Route["API:BaseUrl"] + "/can/");
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpResponseMessage a = await client.PostAsync(uri, data);
            if (a.IsSuccessStatusCode)
            {
                Frame.Navigate(typeof(CansPage));
            } else
            {
                Console.WriteLine(a.StatusCode + ": " + a.Content.ToString());
            }
        }

        private void manufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            box.BorderBrush = null;
            var obj = box.SelectedItem as ManufacturerDTO;
            if(obj.Id.Equals("-1"))
            {
                manuf.Visibility = Visibility.Visible;
                ManufacturerDTO = null;
            } else
            {
                manuf.Visibility = Visibility.Collapsed;
                ManufacturerDTO = obj;
            }
        }

        private void tastescobobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            box.BorderBrush = null;
            var obj = box.SelectedItem as TasteDTO;
            if (obj.Id.Equals("-1"))
            {
                taste.Visibility = Visibility.Visible;
                TasteDTO = null;
            } else
            {
                taste.Visibility = Visibility.Collapsed;
                TasteDTO = obj;
            }
        }

        private void countriescobobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            box.BorderBrush = null;
            var obj = box.SelectedItem as CountryDTO;
            if (obj.Id.Equals("-1"))
            {
                coun.Visibility = Visibility.Visible;
                CountryDTO = null;
            } else
            {
                coun.Visibility = Visibility.Collapsed;
                CountryDTO = obj;
            }
        }

        private void Barcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            var s = sender as TextBox;
            s.BorderBrush = null;
        }
    }
}
