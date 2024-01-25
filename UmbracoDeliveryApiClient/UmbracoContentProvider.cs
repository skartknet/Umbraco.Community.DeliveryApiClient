using Microsoft.Extensions.Caching.Memory;
using UmbracoDeliveryApiClient.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

namespace UmbracoDeliveryApiClient
{
    public class UmbracoContentProvider : IContentProvider
    {
        private readonly UmbracoDeliveryApiContext umbracoDeliveryApiContext;
        private readonly HttpClient httpClient;
        private readonly IMemoryCache memoryCache;



        public UmbracoContentProvider(UmbracoDeliveryApiContext umbracoDeliveryApiContext,
                                        HttpClient httpClient,
                                        IMemoryCache memoryCache)
        {
            this.umbracoDeliveryApiContext = umbracoDeliveryApiContext;
            this.httpClient = httpClient;
            this.memoryCache = memoryCache;
        }

        public async Task<ChildrenCollection<T>> GetChildrenAsync<T>(string pathOrId) where T : IContent
        {
            const string endpoint = "/umbraco/delivery/api/v1/content?fetch=children:{0}";
            var rootId = umbracoDeliveryApiContext.GetCurrentRootId();

            var key = $"{rootId}-{pathOrId}-children";

            ChildrenCollection<T>? result = default;

            if (umbracoDeliveryApiContext.Settings.EnableCache)
            {
                result = await memoryCache.GetOrCreateAsync(key, async entry =>
                {
                    var result = await GetAsync<ChildrenCollection<T>>(endpoint, pathOrId);
                    return result;
                });

                UpdateRootCacheList(rootId, key);

            }
            else
            {
                result = await GetAsync<ChildrenCollection<T>>(endpoint, pathOrId);
            }


            return result;
        }

        public async Task<T> GetContentItemAsync<T>(string pathOrId) where T : IContent
        {
            const string endpoint = "/umbraco/delivery/api/v1/content/item/{0}";
            var rootId = umbracoDeliveryApiContext.GetCurrentRootId();

            var key = $"{rootId}-{pathOrId}";

            T? result = default;

            if (umbracoDeliveryApiContext.Settings.EnableCache)
            {
                result = await memoryCache.GetOrCreateAsync(key, async entry =>
                {
                    var result = await GetAsync<T>(endpoint, pathOrId);
                    return result;
                });

                UpdateRootCacheList(rootId, key);

            }
            else
            {
                result = await GetAsync<T>(endpoint, pathOrId);
            }



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

                    var model = await JsonSerializer.DeserializeAsync<T>(umbracoResponse, options);
                    return model;
                }
            }

            //TODO: Add some loggin about the failed request here

            return default;
        }

        public void RefreshAll(Guid rootId)
        {
            var rootCaches = memoryCache.Get<List<string>>(rootId);

            if (rootCaches is null) return;

            foreach (var item in rootCaches)
            {
                memoryCache.Remove(item);
            }

            memoryCache.Remove(rootId);
        }

        public void RefreshContent(string pathOrId)
        {
            var rootId = umbracoDeliveryApiContext.GetCurrentRootId();

            memoryCache.Remove($"{rootId}-{pathOrId}");
        }

        public void RefreshContent(Guid rootId, string pathOrId)
        {
            //TODO not sure about this children thing. Can we find a better way to define the caching of children?
            memoryCache.Remove($"{rootId.ToString()}-{pathOrId}");
            memoryCache.Remove($"{rootId.ToString()}-{pathOrId}-children");

        }

        // we use the rootCaches to maintain a list of the caches
        // that we need to refresh in case of any changes on content
        private void UpdateRootCacheList(Guid rootId, string key)
        {
            var rootCaches = memoryCache.Get<List<string>>(rootId);

            if (rootCaches is null)
            {
                rootCaches = new List<string>();
            }

            if (rootCaches.Contains(key) == false)
            {
                rootCaches.Add(key);
            }

            memoryCache.Set(rootId, rootCaches);

        }
    }
}
