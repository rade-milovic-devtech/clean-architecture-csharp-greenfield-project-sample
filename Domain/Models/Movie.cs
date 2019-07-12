namespace Domain.Models
{
	public class Movie
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public MoviePrice Price { get; set; }
	}
}