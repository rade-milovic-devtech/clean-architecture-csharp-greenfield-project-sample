using System;
using System.Linq;
using Domain.Models;

namespace Domain
{
	public class BoughtLeastMoviesDuringTheLastPeriod : IDefineCustomerPromotionRules
	{
		private readonly int _count;
		private readonly TimeSpan _pastPeriod;

		public BoughtLeastMoviesDuringTheLastPeriod(int count, TimeSpan pastPeriod)
		{
			_count = count;
			_pastPeriod = pastPeriod;
		}

		public bool IsSatisfiedBy(Customer customer)
		{
			return customer.PurchasedMovies
				    .Count(movie => movie.Purchased >= DateProviderFactory.DateProvider.Now().Subtract(_pastPeriod))
			       >= _count;
		}
	}
}