using System;

namespace Domain.Models
{
	public abstract class PurchasedMovie
	{
		protected PurchasedMovie(MovieOffer movieOffer, IDateProvider dateProvider)
		{
			MovieId = movieOffer.MovieId;
			Purchased = dateProvider.Now();
		}

		public string MovieId { get; set; }
		public decimal Price { get; set; }
		public DateTime Purchased { get; set; }

		public static LifelongPurchasedMovie Lifelong(MovieOffer movieOffer, IDateProvider dateProvider) => new LifelongPurchasedMovie(movieOffer, dateProvider);

		public static TwoDaysPurchasedMovie TwoDays(MovieOffer movieOffer, IDateProvider dateProvider) => new TwoDaysPurchasedMovie(movieOffer, dateProvider);
	}
}