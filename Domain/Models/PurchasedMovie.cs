using System;

namespace Domain.Models
{
	public abstract class PurchasedMovie
	{
		protected PurchasedMovie(MovieOffer movieOffer)
		{
			MovieId = movieOffer.MovieId;
			Purchased = DateProviderFactory.DateProvider.Now();
		}

		public string MovieId { get; set; }
		public decimal Price { get; set; }
		public DateTime Purchased { get; set; }

		public static LifelongPurchasedMovie Lifelong(MovieOffer movieOffer) => new LifelongPurchasedMovie(movieOffer);

		public static TwoDaysPurchasedMovie TwoDays(MovieOffer movieOffer) => new TwoDaysPurchasedMovie(movieOffer);
	}
}