using System;
using Domain.Models;
using Moq;
using Xunit;

namespace Domain.Tests
{
	public class PurchasedMovieTests
	{
		private const decimal _twoDaysPrice = 10.5m;
		private const decimal _lifeLongPrice = 50.5m;

		private readonly Mock<IDateProvider> _dateProviderMock;

		public PurchasedMovieTests()
		{
			_dateProviderMock = new Mock<IDateProvider>();
		}

		[Fact]
		public void LifelongSetsLifelongPrice()
		{
			var movieOffer = new MovieOffer
			{
				MovieId = "123asd",
				Title = "Movie title",
				Price = new MoviePrice(_twoDaysPrice, _lifeLongPrice)
			};

			var purchasedMovie = PurchasedMovie.Lifelong(movieOffer, _dateProviderMock.Object);

			Assert.Equal(_lifeLongPrice, purchasedMovie.Price);
		}

		[Fact]
		public void TwoDaysSetsTwoDaysPrice()
		{
			var movieOffer = new MovieOffer
			{
				MovieId = "123asd",
				Title = "Movie title",
				Price = new MoviePrice(_twoDaysPrice, _lifeLongPrice)
			};

			var purchasedMovie = PurchasedMovie.TwoDays(movieOffer, _dateProviderMock.Object);

			Assert.Equal(_twoDaysPrice, purchasedMovie.Price);
		}

		[Fact]
		public void TwoDaysSetsExpirationTwoDaysInFuture()
		{
			var movieOffer = new MovieOffer
			{
				MovieId = "123asd",
				Title = "Movie title",
				Price = new MoviePrice(_twoDaysPrice, _lifeLongPrice)
			};

			_dateProviderMock.Setup(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 15));

			var purchasedMovie = PurchasedMovie.TwoDays(movieOffer, _dateProviderMock.Object);

			Assert.Equal(new DateTime(2019, 7, 17), purchasedMovie.Expiration);
		}
	}
}