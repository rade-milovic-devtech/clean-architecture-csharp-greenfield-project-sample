namespace Domain.Models
{
	public class MovieOffer
	{
		public string MovieId { get; set; }
		public string Title { get; set; }
		public MoviePrice Price { get; set; }

		public static MovieOffer Create(Movie movie, MoviePriceCalculator moviePriceCalculator)
		{
			return new MovieOffer
			{
				MovieId = movie.Id,
				Title = movie.Title,
				Price = moviePriceCalculator.GetPrice(movie.Price)
			};
		}
	}
}