using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Xunit;

namespace Domain.Tests
{
	public class MovieCatalogTests
	{
		private const string MovieId = "423dsr34f3";
		private const string MovieName = "Catch me if you can";
		private const decimal TwoDaysMoviePrice = 10.4m;
		private const decimal LifeLongMoviePrice= 104.0m;

		private readonly MovieCatalog _movieCatalog;

		public MovieCatalogTests()
		{
			var movies = new List<Movie>{ new Movie
			{
				Id = MovieId,
				Title = MovieName,
				Price = new MoviePrice(TwoDaysMoviePrice, LifeLongMoviePrice)
			}};

			var moviePriceFactory = new MoviePriceCalculatorFactory();

			_movieCatalog = new MovieCatalog(movies, moviePriceFactory);
		}

		[Fact]
		public void GetMoviesWithUnchangedPricesForRegularCustomer()
		{
			var customer = new Customer
			{
				Name = "Milicevic Miroslav",
				Id = "0ffs1",
				Email = "mmd333@fdsfsd.com",
				Status = CustomerStatus.Regular
			};

			var offers = _movieCatalog.GetMovies(customer);

			Assert.Equal(1, offers.Count);
			Assert.Equal(MovieId, offers.First().MovieId);
			Assert.Equal(MovieName, offers.First().Title);
			Assert.Equal(TwoDaysMoviePrice, offers.First().Price.TwoDays);
			Assert.Equal(LifeLongMoviePrice, offers.First().Price.LifeLong);
		}

		[Fact]
		public void GetMoviesWith25PercentDiscountForAdvancedCustomer()
		{
			var customer = new Customer
			{
				Name = "Milicevic Miroslav",
				Id = "0ffs1",
				Email = "mmd333@fdsfsd.com",
				Status = CustomerStatus.Advanced
			};

			var offers = _movieCatalog.GetMovies(customer);

			Assert.Equal(1, offers.Count);
			Assert.Equal(MovieId, offers.First().MovieId);
			Assert.Equal(MovieName, offers.First().Title);
			Assert.Equal(7.8m, offers.First().Price.TwoDays);
			Assert.Equal(78m, offers.First().Price.LifeLong);
		}
	}
}
