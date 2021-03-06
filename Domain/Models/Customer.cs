using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Models
{
	public class Customer
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		private CustomerStatus _status;

		public CustomerStatus Status
		{
			get
			{
				_status = _status.Refresh(this);
				return _status;
			}
			set => _status = value;
		}

		public IReadOnlyList<PurchasedMovie> PurchasedMovies => new ReadOnlyCollection<PurchasedMovie>(_purchasedMovies);
			
		private readonly IList<PurchasedMovie> _purchasedMovies;

		public Customer()
		{
			_status = new CustomerStatusRegular();
			_purchasedMovies = new List<PurchasedMovie>();
		}

		public Customer(CustomerStatus status, IList<PurchasedMovie> purchasedMovies)
		{
			_status = status;
			_purchasedMovies = purchasedMovies;
		}

		public void BuyLifelong(MovieOffer movie)
		{
			_purchasedMovies.Add(PurchasedMovie.Lifelong(movie));
		}

		public void BuyTwoDays(MovieOffer movie)
		{
			_purchasedMovies.Add(PurchasedMovie.TwoDays(movie));
		}
	}
}

