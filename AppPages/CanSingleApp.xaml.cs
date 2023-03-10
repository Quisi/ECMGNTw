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
    public partial class CanSingleApp : Page
    {
        private CanDTO Candto;
        public List<string> Pictures = new List<string>();
        public CanSingleApp()
        {
            Pictures.Add("/Assets/icon_add.png");
            Pictures.Add("/Assets/icon_edit.png");
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Candto = e.Parameter as CanDTO;
            
        }
    }
}
