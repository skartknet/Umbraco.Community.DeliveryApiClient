using Umbraco.Community.DeliveryApiClient.Net.Models;

namespace Umbraco.Community.DeliveryApiClient.Net;

public interface IContentProvider
{

    public Task<T> GetContentItemAsync<T>(string path, Guid? rootId = null) where T : IContent;
    public Task<ChildrenCollection<T>> GetChildrenAsync<T>(string path, Guid? rootId = null) where T : IContent;    
}
