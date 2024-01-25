using Microsoft.Extensions.DependencyInjection;
using UmbracoDeliveryApiClient.Settings;

namespace UmbracoDeliveryApiClient.Web.Extensions
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
