using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using UmbracoDeliveryApiClient.Settings;


namespace UmbracoDeliveryApiClient
{
    public class UmbracoDeliveryApiContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UmbracoApiOptions Settings { get; }

        public UmbracoDeliveryApiContext(IHttpContextAccessor httpContextAccessor,
                                        IOptions<UmbracoApiOptions> umbracoApiOptions,
                                        HttpClient httpClient)
        {
            this.httpContextAccessor = httpContextAccessor;
            Settings = umbracoApiOptions.Value;
            RequestedDomain = httpContextAccessor?.HttpContext?.Request?.Host.Value ?? throw new ApplicationException("Couldn't find a valid host value.");

            if (umbracoApiOptions.Value.Domain is null)
            {
                throw new ArgumentNullException(nameof(umbracoApiOptions.Value.Domain));
            }
            else
            {
                UmbracoDomain = new Uri(umbracoApiOptions.Value.Domain);
            }


            httpClient.BaseAddress = UmbracoDomain;

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Api-Key", umbracoApiOptions.Value.ApiKey);
            httpClient.DefaultRequestHeaders.Add("Start-Item", GetCurrentRootId().ToString());


        }

        public Dictionary<string, string> DomainsRootRelations { get; } = new Dictionary<string, string>();
        public string RequestedDomain { get; }
        public Uri UmbracoDomain { get; }

        public void AddDomainRootRelation(string domain, string root)
        {
            if (DomainsRootRelations.ContainsKey(domain))
                DomainsRootRelations[domain] = root;
            else
                DomainsRootRelations.Add(domain, root);
        }

        public Guid GetCurrentRootId()
        {
            // Temporarily harcoded just for VIC

            return Guid.Parse("089869fc-2452-43a2-b22a-c0320af67d8b");


            //if (DomainsRootRelations.ContainsKey(RequestedDomain))
            //{
            //	var root = DomainsRootRelations.GetValueOrDefault(RequestedDomain);
            //	return root is null ? throw new Exception("Requested domain is not registered with a valid root.") : root;
            //}
            //else
            //{
            //	throw new Exception("Requested domain is not registered with a valid root.");
            //}
        }

    }
}
