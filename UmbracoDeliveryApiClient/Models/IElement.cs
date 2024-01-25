namespace UmbracoDeliveryApiClient.Models

{
    public interface IElement
    {
        string ContentType { get; set; }
        Guid Id { get; set; }
    }
}
