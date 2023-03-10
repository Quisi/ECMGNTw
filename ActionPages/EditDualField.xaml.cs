using EnergyScanVerwaltung;
using EnergyScanVerwaltung.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class EditDualField : Page
    {
        private ApplicationDataContainer localSettings;
        private string edittype;
        private Type sourcePage = typeof(UserProfile);
        public EditDualField()
        {
            this.InitializeComponent();
            localSettings = ApplicationData.Current.LocalSettings;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            Type t = (e.Parameter != null) ? e.Parameter.GetType() : null;
            if (t != null)
            {
                if (t.Equals(typeof(Status)))
                {
                    sourcePage = typeof(StatiPage);
                    var s = e.Parameter as Status;
                    editid.Text = s.id;
                    editname.Text = s.name;
                    edittitle.Text = "Edit Status";
                    edittype = "status";
                }
            }
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            string json = "";
            Uri uri = new Uri(Root.Config.Route["API:BaseUrl"]);
            switch (edittype)
            {
                case "status":
                Status s = new Status() { id = editid.Text, name = editname.Text };
                json = JsonConvert.SerializeObject(s);
                uri = new Uri(Root.Config.Route["API:BaseUrl"] + "/status/" + s.id);
                break;
                case "":
                break;
                default:
                return;
            }
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", localSettings.Values["token"].ToString());
            var a = await client.PutAsync(uri, data);
            if(a.IsSuccessStatusCode)
            {
                Frame.Navigate(sourcePage);
            }
            else
            {
                Console.WriteLine(a.StatusCode + ": " + a.Content.ToString());
            }
        }
    }
}
