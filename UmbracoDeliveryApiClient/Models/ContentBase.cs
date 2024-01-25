namespace UmbracoDeliveryApiClient.Models

{
	public abstract class ContentBase<T> : IContent
	{

		public Guid Id { get; set; }
		public string ContentType { get; set; } = "";
		public string Name { get; set; } = "";
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }

		public T? Properties { get; set; }

	}

	public class Content : ContentBase<object>
	{
	}
}
