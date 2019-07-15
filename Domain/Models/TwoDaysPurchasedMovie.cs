using System;

namespace Domain.Models
{
	public class TwoDaysPurchasedMovie : PurchasedMovie
	{
		public TwoDaysPurchasedMovie(MovieOffer movieOffer, IDateProvider dateProvider) : base(movieOffer, dateProvider)
		{
			Price = movieOffer.Price.TwoDays;
			Expiration = dateProvider.Now().AddDays(2);
		}

		public DateTime Expiration { get; set; }
	}
}