using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Models
{
	public class Customer
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public CustomerStatus Status { get; set; }
		public IReadOnlyList<PurchasedMovie> PurchasedMovies => new ReadOnlyCollection<PurchasedMovie>(_purchasedMovies);
			
		private readonly IList<PurchasedMovie> _purchasedMovies = new List<PurchasedMovie>();

		public void BuyLifelong(MovieOffer movie, IDateProvider dateProvider)
		{
			_purchasedMovies.Add(PurchasedMovie.Lifelong(movie, dateProvider));
		}

		public void BuyTwoDays(MovieOffer movie, IDateProvider dateProvider)
		{
			_purchasedMovies.Add(PurchasedMovie.TwoDays(movie, dateProvider));
		}
	}
}

