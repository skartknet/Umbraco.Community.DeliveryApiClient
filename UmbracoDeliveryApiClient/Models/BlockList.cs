
namespace Umbraco.Community.DeliveryApiClient.Net.Models;


public class BlockList<T> where T : IElement
{
    public List<BlockListItem<T>> Items { get; set; } = new List<BlockListItem<T>>();
}
