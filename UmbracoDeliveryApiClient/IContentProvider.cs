using UmbracoDeliveryApiClient.Models;

namespace UmbracoDeliveryApiClient
{
    public interface IContentProvider
    {

        public Task<T> GetContentItemAsync<T>(string path) where T : IContent;
        public Task<ChildrenCollection<T>> GetChildrenAsync<T>(string path) where T : IContent;
        void RefreshContent(Guid rootId, string pathOrId);
        void RefreshAll(Guid rootId);
    }
}
