using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Application.Customers.ViewCatalog
{
	public class MovieCatalogDtoMapper
	{
		public static MovieCatalogDto MapToDto(IList<MovieOffer> movieOffers)
		{
			return new MovieCatalogDto
			{
				Movies = movieOffers.Select(movie => new MovieOfferDto
				{
					Id = movie.MovieId,
					Title = movie.Title,
					LifelongPrice = movie.Price.LifeLong,
					TwoDaysPrice = movie.Price.TwoDays
				}).ToList()
			};
		}
	}
}