using System;
using System.Linq;
using Domain.Models;

namespace Domain
{
	public class SpentLeastAmountDuringTheLastPeriod : IDefineCustomerPromotionRules
	{
		private readonly decimal _amount;
		private readonly TimeSpan _pastPeriod;

		public SpentLeastAmountDuringTheLastPeriod(decimal amount, TimeSpan pastPeriod)
		{
			_amount = amount;
			_pastPeriod = pastPeriod;
		}

		public bool IsSatisfiedBy(Customer customer)
		{
			return customer.PurchasedMovies
				       .Where(movie => movie.Purchased >= DateProviderFactory.DateProvider.Now().Subtract(_pastPeriod))
				       .Sum(movie => movie.Price)
			       >= _amount;
		}
	}
}