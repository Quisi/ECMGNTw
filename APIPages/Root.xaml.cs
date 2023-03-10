using EnergyScanVerwaltung;
using EnergyScanVerwaltung.AppPages;
using EnergyScanVerwaltung.DTOs;
using EnergyScanVerwaltung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EnergyScanVerwaltung
{
    /// <summary>
    /// A starting page with a Frame.
    /// </summary>
    public sealed partial class Root : Page
    {
        public static TokenDTO tokenDTO;
        public static AppConfig Config;
        
        public ApplicationDataContainer localSettings;
        public Root()
        {
            InitializeComponent();
            localSettings = ApplicationData.Current.LocalSettings;
            Config = new AppConfig();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            tokenDTO = e.Parameter as TokenDTO;
            if (tokenDTO is null)
            {
                Frame.Navigate(typeof(Login));
            }
        }

        
        private double NavViewCompactModeThresholdWidth => NavView.CompactModeThresholdWidth;

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>()
        {
            ("home", typeof(UserProfile)),
            ("barcodes", typeof(BarcodesPage)),
            ("cans", typeof(CansPage)),
            ("countrys", typeof(CountrysPage)),
            ("groups", typeof(GroupsPage)),
            ("manufacturers", typeof(ManufacturersPage)),
            ("policys", typeof(PolicysPage)),
            ("stati", typeof(StatiPage)),
            ("tags", typeof(TagsPage)),
            ("tastes", typeof(TastesPage)),
            ("changerequests", typeof(ChangeRequestPage)),

            ("appchangerequest", typeof(ChangeRequestApp)),
            ("appcans", typeof(CanApp))
            
        };

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // You can also add items in code.
            /*
            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItem
            {
                Content = "Changerequests",
                Icon = new SymbolIcon((Symbol)0xF1AD),
                Tag = "changerequests"
            });
            _pages.Add(("changerequests", typeof(ChangeRequestPage)));
            */

            // Add handler for ContentFrame navigation.
            ContentFrame.Navigated += On_Navigated;

            // NavView doesn't load any page by default, so load home page.
            //NavView.SelectedItem = NavView.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("home");

            // Listen to the window directly so the app responds
            // to accelerator keys regardless of which element has focus.
            /*
            */
            //Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

            //SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
        }

        private void NavView_ItemInvoked(NavigationView sender,
                                         NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavView_Navigate("settings");
            } else if (args.InvokedItemContainer != null)
            {
                string navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag);
            }
        }

        private void NavView_Navigate(string navItemTag)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                _page = typeof(Settings);
            } else
            {
                (string Tag, Type Page) item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            Type preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null);
            }
        }

        private void NavView_BackRequested(NavigationView sender,
                                           NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        

        private void System_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Handle mouse back button.
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                e.Handled = TryGoBack();
            }
        }

        private bool TryGoBack()
        {
            
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
            
            //return false; // disable go-back function
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(Settings))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                //NavView.Header = "Settings";
            } else if (ContentFrame.SourcePageType != null)
            {
                /*(string Tag, Type Page) item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));
                */
                //NavView.Header = ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
            
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = dataset;
            }
        }


        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
        }


        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
            } else
            {
                // Use args.QueryText to determine what to do.
            }
        }
    }
}
