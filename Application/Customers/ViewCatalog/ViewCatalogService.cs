using System.Collections.Generic;
using Application.Ports;
using CSharpFunctionalExtensions;
using Domain;

namespace Application.Customers.ViewCatalog
{
	public class ViewCatalogService
	{
		private readonly IGetCustomers _customersGetter;
		private readonly IGetMovies _moviesGetter;

		public ViewCatalogService(IGetCustomers customersGetter, IGetMovies moviesGetter)
		{
			_customersGetter = customersGetter;
			_moviesGetter = moviesGetter;
		}

		public Result<MovieCatalogDto> GetCatalog(string customerId)
		{
			var customer = _customersGetter.Get(customerId);
			if (customer.HasNoValue)
			{
				return Result.Fail<MovieCatalogDto>(Errors.CustomerNotFound);
			}

			var movies = _moviesGetter.GetAll();

			var movieCatalog = new MovieCatalog(movies, new MoviePriceCalculatorFactory());
			var movieOffers = movieCatalog.GetMovies(customer.Value);

			return Result.Ok(MovieCatalogDtoMapper.MapToDto(movieOffers));
		}
	}

	public class MovieCatalogDto
	{
		public IList<MovieOfferDto> Movies { get; set; }
	}

	public class MovieOfferDto
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public decimal TwoDaysPrice { get; set; }
		public decimal LifelongPrice { get; set; }
	}
}