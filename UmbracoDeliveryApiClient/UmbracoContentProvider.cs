using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Umbraco.Community.DeliveryApiClient.Net.Models;

namespace Umbraco.Community.DeliveryApiClient.Net;

public class UmbracoContentProvider : IContentProvider
{
    private readonly UmbracoDeliveryApiContext umbracoDeliveryApiContext;
    private readonly HttpClient httpClient;



    public UmbracoContentProvider(UmbracoDeliveryApiContext umbracoDeliveryApiContext,
                                    HttpClient httpClient)
    {
        this.umbracoDeliveryApiContext = umbracoDeliveryApiContext;
        this.httpClient = httpClient;

        httpClient.BaseAddress = umbracoDeliveryApiContext.UmbracoDomain;

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Api-Key", umbracoDeliveryApiContext.Settings.ApiKey);


    }

    public async Task<ChildrenCollection<T>> GetChildrenAsync<T>(string pathOrId, Guid? rootId) where T : IContent
    {
        const string endpoint = "/umbraco/delivery/api/v1/content?fetch=children:{0}&take=100";

        var key = rootId.HasValue ? $"{rootId}-{pathOrId}-children" : $"{pathOrId}-children";

        ChildrenCollection<T>? result = default;


        result = await GetAsync<ChildrenCollection<T>>(endpoint, pathOrId);

        return result;
    }

    public async Task<T> GetContentItemAsync<T>(string pathOrId, Guid? rootId = null) where T : IContent
    {
        const string endpoint = "/umbraco/delivery/api/v1/content/item/{0}";

        T? result = default;

        result = await GetAsync<T>(endpoint, pathOrId);

        return result;
    }

    private async Task<T?> GetAsync<T>(string endpoint, string pathOrId)
    {
        var url = new Uri(umbracoDeliveryApiContext.UmbracoDomain, string.Format(endpoint, pathOrId));

        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        var response = httpClient.GetAsync(url).Result;

        if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
        {
            var umbracoResponse = await response.Content.ReadAsStreamAsync();

            if (umbracoResponse is not null)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    }
                };

                try
                {
                    var model = await JsonSerializer.DeserializeAsync<T>(umbracoResponse, options);
                    return model;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //TODO: Add some loggin about the failed request here

        return default;
    }


}
