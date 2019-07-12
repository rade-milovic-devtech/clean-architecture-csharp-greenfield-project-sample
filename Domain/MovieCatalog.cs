using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

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

			return _movies.Select(m => new MovieOffer
			{
				Id = m.Id,
				Name = m.Name,
				Price = priceCalculator.GetPrice(m.Price)
			}).ToList();
		}
	}
}