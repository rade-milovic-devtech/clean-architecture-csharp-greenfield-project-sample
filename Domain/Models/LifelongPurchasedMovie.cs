namespace Domain.Models
{
	public class LifelongPurchasedMovie : PurchasedMovie
	{
		public LifelongPurchasedMovie(MovieOffer movieOffer, IDateProvider dateProvider) : base(movieOffer, dateProvider)
		{
			Price = movieOffer.Price.LifeLong;
		}
	}
}