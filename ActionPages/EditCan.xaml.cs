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
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace EnergyScanVerwaltung.ActionPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class EditCan : Page
    {
        CanDTO oldCanDTO;
        CanDTO CanDTO;
        List<BarcodeDTO> BarcodeDTOs;
        ManufacturerDTO ManufacturerDTO;
        TasteDTO TasteDTO;
        CountryDTO CountryDTO;
        StatusDTO StatusDTO;
        List<TagDTO> TagDTOs;

        public EditCan()
        {
            this.InitializeComponent();
            /*
            if (CanDTO != null)
            {
                BarcodeDTOs = CanDTO.Barcodes;
                ManufacturerDTO = CanDTO.Manufacturer;
                TasteDTO = CanDTO.Taste;
                CountryDTO = CanDTO.Country;
                StatusDTO = CanDTO.Status;
            }
            */
            //GetCanInfo();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CanDTO = new CanDTO();
            oldCanDTO = e.Parameter as CanDTO;
            if (oldCanDTO != null)
            {
                CanDTO.Closure = oldCanDTO.Closure;
                CanDTO.Coffeincontent = oldCanDTO.Coffeincontent;
                CanDTO.Contentamount = oldCanDTO.Contentamount;
                CanDTO.Country = oldCanDTO.Country;
                CanDTO.Damaged = oldCanDTO.Damaged;
                CanDTO.Deposit = oldCanDTO.Deposit;
                CanDTO.Description = oldCanDTO.Description;
                CanDTO.Manufacturer = oldCanDTO.Manufacturer;
                CanDTO.Mhd = oldCanDTO.Mhd;
                CanDTO.Status = oldCanDTO.Status;
                CanDTO.Taste = oldCanDTO.Taste;
                CanDTO.Barcodes = BarcodeDTOs = oldCanDTO.Barcodes;
                CanDTO.Tags = TagDTOs = oldCanDTO.Tags;
            }
        }

        private async void GetCanInfo()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Root.Config.Route["API:BaseUrl"] + "/can/"),
            };
            HttpResponseMessage response = await client.SendAsync(httpRequest);
            string content = await response.Content.ReadAsStringAsync();
            if(!string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    CanDTO = JsonConvert.DeserializeObject<CanDTO>(content);
                }catch(Exception e)
                {
                    Console.WriteLine("jsonexception: " + e.Message);
                }
            }
            BarcodeDTOs = CanDTO.Barcodes;
            ManufacturerDTO = CanDTO.Manufacturer;
            TasteDTO = CanDTO.Taste;
            CountryDTO = CanDTO.Country;
            StatusDTO = CanDTO.Status;
            TagDTOs = CanDTO.Tags;
        }

        private async void Create_ClickAsync(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Root.tokenDTO.access_token);
            Uri uri = new Uri(Root.Config.Route["API:BaseUrl"]);
            StringContent data = new StringContent("");
            bool error = false;
            if (CanDTO.Closure != oldCanDTO.Closure)
            {
                //create changerequest closure
                CanPatchDTO cpd = new CanPatchDTO();
                cpd.FieldName = "Closure";
                cpd.NewValue = CanDTO.Closure;
                string json = JsonConvert.SerializeObject(cpd);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                uri = new Uri(Root.Config.Route["API:BaseUrl"] + "/can/patch/"+oldCanDTO.Id);
                HttpResponseMessage a = await client.PutAsync(uri, data);
                if (!a.IsSuccessStatusCode)
                {
                    error = true;
                }
                
            }
            if(CanDTO.Coffeincontent != oldCanDTO.Coffeincontent)
            {

            }
            if(CanDTO.Contentamount != oldCanDTO.Contentamount)
            {

            }
            if(CanDTO.Country.Name != oldCanDTO.Country.Name)
            {

            }
            if(CanDTO.Damaged != oldCanDTO.Damaged)
            {

            }
            if(CanDTO.Deposit != oldCanDTO.Deposit)
            {

            }
            if(CanDTO.Description != oldCanDTO.Description)
            {

            }
            if(CanDTO.Manufacturer.Name != oldCanDTO.Manufacturer.Name)
            {

            }
            if(CanDTO.Mhd != oldCanDTO.Mhd)
            {

            }
            if(CanDTO.Status.Name != oldCanDTO.Status.Name)
            {

            }
            if(CanDTO.Taste.Name != oldCanDTO.Taste.Name)
            {

            }
            foreach(BarcodeDTO barcode in CanDTO.Barcodes)
            {
                if(!oldCanDTO.Barcodes.Exists(b => b.Name.Equals(barcode.Name)))
                {
                    // create changerequest for this barcode.Id

                }
            }
            foreach(TagDTO tag in CanDTO.Tags)
            {
                if(!oldCanDTO.Tags.Exists(t => t.Name.Equals(tag.Name)))
                {
                    //create changerequest for this tag.Id

                }
            }

            /*
            CanDTO.Barcodes = BarcodeDTOs;
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
            */
        }
    }
}
