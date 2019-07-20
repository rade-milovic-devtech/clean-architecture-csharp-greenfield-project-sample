using System;
using System.Collections.Generic;
using System.ComponentModel;
using Application.Ports;
using CSharpFunctionalExtensions;
using Domain;
using Domain.Models;

namespace Application.Customers.BuyMovie
{
	public class BuyMovieService
	{
		private readonly IGetCustomers _customerGetter;
		private readonly IGetMovies _movieGetter;
		private readonly MoviePriceCalculatorFactory _moviePriceCalculatorFactory;

		public BuyMovieService(IGetCustomers customerGetter, IGetMovies movieGetter,
			MoviePriceCalculatorFactory moviePriceCalculatorFactory)
		{
			_customerGetter = customerGetter;
			_movieGetter = movieGetter;
			_moviePriceCalculatorFactory = moviePriceCalculatorFactory;
		}

		public void BuyMovie(BuyMovieDto buyMovieRequest, IEnumerable<IHandleMoviePurchased> moviePurchasedHandlers)
		{
			var customer = _customerGetter.Get(buyMovieRequest.CustomerId).ToResult(Errors.CustomerNotFound);
			var movie = _movieGetter.Get(buyMovieRequest.MovieId).ToResult(Errors.MovieNotFound);

			Result.Combine(customer, movie)
				.OnSuccess(() =>
				{
					var moviePriceCalculator = _moviePriceCalculatorFactory.GetPriceCalculatorFor(customer.Value);

					var offer = MovieOffer.Create(movie.Value, moviePriceCalculator);
					var buyMethod = GetBuyMethod(buyMovieRequest.MovieLicenseType, customer.Value);

					buyMethod(offer);

					HandleMoviePurchased(moviePurchasedHandlers, Result.Ok<MovieOffer>(offer));
				})
				.OnFailure(message => HandleMoviePurchased(moviePurchasedHandlers, Result.Fail<MovieOffer>(message)));
		}

		private Action<MovieOffer> GetBuyMethod(MovieLicenseType movieLicenseType, Maybe<Customer> customer)
		{
			switch (movieLicenseType)
			{
				case MovieLicenseType.Lifelong:
					return customer.Value.BuyLifelong;
				case MovieLicenseType.TwoDays:
					return customer.Value.BuyTwoDays;
				default:
					throw new InvalidEnumArgumentException();
			}
		}

		private void HandleMoviePurchased(IEnumerable<IHandleMoviePurchased> moviePurchasedHandlers, Result<MovieOffer> movieOffer)
		{
			foreach (var handler in moviePurchasedHandlers)
			{
				handler.Handle(movieOffer);
			}
		}
	}

	public class BuyMovieDto
	{
		public string CustomerId { get; set; }
		public string MovieId { get; set; }
		public MovieLicenseType MovieLicenseType { get; set; }
	}
}
