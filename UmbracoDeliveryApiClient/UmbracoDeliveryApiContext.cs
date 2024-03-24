using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Umbraco.Community.DeliveryApiClient.Net.Settings;


namespace Umbraco.Community.DeliveryApiClient.Net;

public class UmbracoDeliveryApiContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    public UmbracoApiOptions Settings { get; }
    public Uri UmbracoDomain { get; }


    public UmbracoDeliveryApiContext(IOptions<UmbracoApiOptions> umbracoApiOptions)
    {
        this.httpContextAccessor = httpContextAccessor;
        Settings = umbracoApiOptions.Value;

        if (umbracoApiOptions.Value.Domain is null)
        {
            throw new ArgumentNullException(nameof(umbracoApiOptions.Value.Domain));
        }
        else
        {
            UmbracoDomain = new Uri(umbracoApiOptions.Value.Domain);
        }


     


    }



   

}
