namespace Umbraco.Community.DeliveryApiClient.Net.Models;


public interface IElement
{
    string ContentType { get; set; }
    Guid Id { get; set; }
}
