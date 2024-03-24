namespace Umbraco.Community.DeliveryApiClient.Net.Models;



public class BlockListItem<T> where T : IElement
{
    public T? Content { get; set; }
}

