using EnergyScanVerwaltung.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace EnergyScanVerwaltung.ActionPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CreateCan : Page
    {
        CanDTO CanDTO;
        BarcodeDTO BarcodeDTO;
        ManufacturerDTO ManufacturerDTO;
        TasteDTO TasteDTO;
        CountryDTO CountryDTO;
        StatusDTO StatusDTO;

        public CreateCan()
        {
            this.InitializeComponent();
            CanDTO = new CanDTO() { Id = "1" };
            BarcodeDTO = new BarcodeDTO() { Id = "1" };
            ManufacturerDTO = new ManufacturerDTO() { Id = "1" };
            TasteDTO = new TasteDTO() { Id = "1" };
            CountryDTO = new CountryDTO() { Id = "1" };
            StatusDTO = new StatusDTO() { Id = "1", Name = "new" };
        }

        private async void Create_ClickAsync(object sender, RoutedEventArgs e)
        {
            CanDTO.Barcodes = new List<BarcodeDTO>() { BarcodeDTO };
            CanDTO.Manufacturer = ManufacturerDTO;
            CanDTO.Taste = TasteDTO;
            CanDTO.Country = CountryDTO;
            CanDTO.Status = StatusDTO;
            CanDTO.Id = "1";
            string json = JsonConvert.SerializeObject(CanDTO);
            Uri uri = new Uri(Root.Config.Route["API:BaseUrl"] + "/app/can/");
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
    }
}
