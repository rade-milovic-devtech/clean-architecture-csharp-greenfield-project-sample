using System;

namespace Domain.Models
{
	public class TwoDaysPurchasedMovie : PurchasedMovie
	{
		public TwoDaysPurchasedMovie(MovieOffer movieOffer) : base(movieOffer)
		{
			Price = movieOffer.Price.TwoDays;
			Expiration = DateProviderFactory.DateProvider.Now().AddDays(2);
		}

		public DateTime Expiration { get; set; }
	}
}