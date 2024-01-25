namespace UmbracoDeliveryApiClient.Models

{
	public class Element<TProperties> : IElement
	{

		public string ContentType { get; set; } = "";

		public Guid Id { get; set; }		

	}
}
