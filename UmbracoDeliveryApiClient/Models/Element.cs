namespace Umbraco.Community.DeliveryApiClient.Net.Models;


public class Element<TProperties> : IElement
{

    public string ContentType { get; set; } = "";

    public Guid Id { get; set; }

}
