namespace Domain.Models
{
	public class MovieOffer
	{
		public string MovieId { get; set; }
		public string Title { get; set; }
		public MoviePrice Price { get; set; }
	}
}