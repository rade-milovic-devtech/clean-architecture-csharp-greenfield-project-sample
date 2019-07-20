using System.Linq;
using Domain.Models;
using Moq;
using Xunit;

namespace Domain.Tests
{
	public class CustomerTests
	{
		public CustomerTests()
		{
			DateProviderFactory.DateProvider = Mock.Of<IDateProvider>();
		}

		[Fact]
		public void BuyTwoDaysMovieAddsItToThePurchasedHistory()
		{
			var customer = new Customer
			{
				Id = "abc",
				Status = new CustomerStatusRegular(),
			};

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(12m, 1m),
				Title = "movie title",
				MovieId = "cfa"
			};

			customer.BuyTwoDays(movieOffer);
			Assert.Equal(1, customer.PurchasedMovies.Count);
			Assert.IsType<TwoDaysPurchasedMovie>(customer.PurchasedMovies.First());

		}

		[Fact]
		public void BuyLifelongMovieAddsItToThePurchasedHistory()
		{
			var customer = new Customer
			{
				Id = "abc",
				Status = new CustomerStatusRegular(),
			};

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(12m, 1m),
				Title = "movie title",
				MovieId = "cfa"
			};

			customer.BuyLifelong(movieOffer);
			Assert.Equal(1, customer.PurchasedMovies.Count);
			Assert.IsType<LifelongPurchasedMovie>(customer.PurchasedMovies.First());

		}
	}
}