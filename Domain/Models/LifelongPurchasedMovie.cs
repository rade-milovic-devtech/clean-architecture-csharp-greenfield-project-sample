namespace Domain.Models
{
	public class LifelongPurchasedMovie : PurchasedMovie
	{
		public LifelongPurchasedMovie(MovieOffer movieOffer) : base(movieOffer)
		{
			Price = movieOffer.Price.LifeLong;
		}
	}
}