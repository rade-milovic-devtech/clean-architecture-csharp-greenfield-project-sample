using System;
using System.Collections.Generic;
using Domain.Models;
using Moq;
using Xunit;

namespace Domain.Tests
{
	public class CustomerStatusTests
	{
		private readonly Mock<IDateProvider> _mockDateProvider;

		public CustomerStatusTests()
		{
            _mockDateProvider = new Mock<IDateProvider>();
			DateProviderFactory.DateProvider = _mockDateProvider.Object;
		}

        [Fact]
		public void IsNotPromotedWhenThereAreNoPurchases()
		{
			var customer = new Customer();

            var status = customer.Status;

            Assert.IsType<CustomerStatusRegular>(status);
        }

		[Fact]
		public void IsNotPromotedWhenThereIsOnlyOnePurchaseInLastMonth()
		{
			_mockDateProvider.Setup(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 20));

            var movieOffer = new MovieOffer
            {
                Price = new MoviePrice(150m, 300m)
            };

			var purchasedMovies = new List<PurchasedMovie>
			{
				new LifelongPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2019, 6, 21)
				},
				new LifelongPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2019, 5, 21)
				}
			};

			var customer = new Customer(new CustomerStatusRegular(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusRegular>(status);
		}

		[Fact]
		public void IsNotPromotedWhen100DollarsHasNotBeenSpentInLastYear()
		{
			_mockDateProvider.Setup(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 20));

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(15m, 30m)
			};

			var purchasedMovies = new List<PurchasedMovie>
			{
				new TwoDaysPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2019, 6, 21)
				},
				new LifelongPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2019, 6, 23)
				}
			};

			var customer = new Customer(new CustomerStatusRegular(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusRegular>(status);
		}

		[Fact]
		public void IsPromotedToAdvancedWhenTwoMoviesAreBoughLastMonthAnd100DollarsIsSpentDuringLastYear()
		{
			_mockDateProvider.Setup(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 20));

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(150m, 300m)
			};

			var purchasedMovies = new List<PurchasedMovie>
			{
				new TwoDaysPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2019, 6, 21)
				},
				new LifelongPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2019, 6, 23)
				}
			};

			var customer = new Customer(new CustomerStatusRegular(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusAdvanced>(status);
		}

		[Fact]
		public void HasNotBeingDemotedToRegularWhenOneYearHasNotPassed()
		{
			_mockDateProvider.SetupSequence(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 5, 20))
				.Returns(new DateTime(2020, 5, 20))
				.Throws<Exception>();
			var purchasedMovies = new List<PurchasedMovie>();

			var customer = new Customer(new CustomerStatusAdvanced(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusAdvanced>(status);
		}

		[Fact]
		public void RenewsAdvancedStatusAfterExpirationWhenTwoMoviesAreBoughLastMonthAnd100DollarsIsSpentDuringLastYear()
		{
			_mockDateProvider.SetupSequence(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 20))
				.Returns(new DateTime(2020, 6, 21))
				.Returns(new DateTime(2020, 6, 23))
				.Returns(new DateTime(2019, 7, 20))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Throws<Exception>();

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(150m, 300m)
			};

			var purchasedMovies = new List<PurchasedMovie>
			{
				new TwoDaysPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2020, 6, 21)
				},
				new LifelongPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2020, 6, 23)
				}
			};

			var customer = new Customer(new CustomerStatusAdvanced(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusAdvanced>(status);
            Assert.Equal(new DateTime(2021, 7, 21), status.ValidTo);
		}


		[Fact]
		public void DemotesToRegularStatusAfterExpirationWhenTwoMoviesAreNotBoughLastMonth()
		{
			_mockDateProvider.SetupSequence(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 20))
				.Returns(new DateTime(2020, 6, 21))
				.Returns(new DateTime(2019, 7, 20))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Throws<Exception>();

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(150m, 300m)
			};

			var purchasedMovies = new List<PurchasedMovie>
			{
				new TwoDaysPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2020, 6, 21)
				}
			};

			var customer = new Customer(new CustomerStatusAdvanced(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusRegular>(status);
		}

		[Fact]
		public void DemotesToRegularStatusAfterExpirationWhen100DollarsWereNotSpentDuringLastYear()
		{
			_mockDateProvider.SetupSequence(dateProvider => dateProvider.Now())
				.Returns(new DateTime(2019, 7, 20))
				.Returns(new DateTime(2020, 6, 21))
				.Returns(new DateTime(2020, 6, 23))
				.Returns(new DateTime(2019, 7, 20))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Returns(new DateTime(2020, 7, 21))
				.Throws<Exception>();

			var movieOffer = new MovieOffer
			{
				Price = new MoviePrice(15m, 30m)
			};

			var purchasedMovies = new List<PurchasedMovie>
			{
				new TwoDaysPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2020, 6, 21)
				},
				new LifelongPurchasedMovie(movieOffer)
				{
					Purchased = new DateTime(2020, 6, 23)
				}
			};

			var customer = new Customer(new CustomerStatusAdvanced(), purchasedMovies);

			var status = customer.Status;

			Assert.IsType<CustomerStatusRegular>(status);
		}
	}
}