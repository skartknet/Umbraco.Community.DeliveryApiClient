namespace UmbracoDeliveryApiClient.Models

{
    public interface IContent : IContentBase
    {
        string ContentType { get; set; }
    }

    public interface IMedia : IContentBase
    {
        string MediaTypeAlias { get; set; }
    }

    public interface IContentBase
    {
        Guid Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
        string Name { get; set; }
    }
}
