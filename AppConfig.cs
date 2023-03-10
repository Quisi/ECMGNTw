using Microsoft.Extensions.Configuration;
using Windows.ApplicationModel;

namespace EnergyScanVerwaltung
{
    public class AppConfig
    {
        public readonly IConfigurationRoot Route;
        public AppConfig()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Package.Current.InstalledLocation.Path)
                                .AddJsonFile("appsettings.json", optional: false);

            Route = builder.Build();   
        }
    }
}
