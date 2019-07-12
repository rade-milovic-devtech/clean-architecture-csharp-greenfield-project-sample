using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Domain
{
	public class MovieCatalog
	{
		private readonly IEnumerable<Movie> _movies;
		private readonly MoviePriceCalculatorFactory _priceCalculatorFactory;

		public MovieCatalog(IEnumerable<Movie> movies, MoviePriceCalculatorFactory priceCalculatorFactory)
		{
			_movies = movies;
			_priceCalculatorFactory = priceCalculatorFactory;
		}

		public IList<MovieOffer> GetMovies(Customer customer)
		{
			var priceCalculator = _priceCalculatorFactory.GetPriceCalculatorFor(customer);

			return _movies.Select(movie => new MovieOffer
			{
				MovieId = movie.Id,
				Title = movie.Title,
				Price = priceCalculator.GetPrice(movie.Price)
			}).ToList();
		}
	}
}