using Microsoft.Extensions.DependencyInjection;
using Umbraco.Community.DeliveryApiClient.Net;
using Umbraco.Community.DeliveryApiClient.Net.Settings;


namespace Umbraco.Community.DeliveryApiClient.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUmbracoDeliveryApi(this IServiceCollection services)
        {
            services.AddSingleton<IContentProvider, UmbracoContentProvider>();
            services.AddSingleton<UmbracoDeliveryApiContext>();


            var builder = services.AddOptions<UmbracoApiOptions>()
                            .BindConfiguration(UmbracoApiOptions.SectionName)
                            .ValidateDataAnnotations();

            return services;
        }
    }
}
