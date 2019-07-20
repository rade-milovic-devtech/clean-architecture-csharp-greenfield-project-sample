using System;
using System.Linq;

namespace Domain.Models
{
	public abstract class CustomerStatus
	{
		public string Name { get; }
        public DateTime? ValidTo { get; }

        protected CustomerStatus(string name, DateTime? validTo = null)
        {
	        Name = name;
	        ValidTo = validTo;
        }

        public abstract CustomerStatus NextStatus(Customer customer);

		protected bool HasBoughtTwoMoviesDuringTheLastMonth(Customer customer, DateTime currentDate)
		{
			return customer.PurchasedMovies
				       .Count(movie => movie.Purchased >= currentDate.AddMonths(-1))
			       >= 2;
		}

		protected bool HasSpentAtLeast100DuringTheLastYear(Customer customer, DateTime currentDate)
		{
			return customer.PurchasedMovies
				       .Where(movie => movie.Purchased >= currentDate.AddYears(-1))
				       .Sum(movie => movie.Price)
			       >= 100;
		}
	}

	public static class CustomerStatusName
	{
		public static string Regular = "Regular";
		public static string Advanced = "Advanced";
	}

	public class CustomerStatusRegular : CustomerStatus
    {
	    public CustomerStatusRegular() : base(CustomerStatusName.Regular) {}

	    public override CustomerStatus NextStatus(Customer customer)
	    {
		    var currentDate = DateProviderFactory.DateProvider.Now();

		    var customerShouldBePromoted = HasBoughtTwoMoviesDuringTheLastMonth(customer, currentDate)
			                               && HasSpentAtLeast100DuringTheLastYear(customer, currentDate);

			if (customerShouldBePromoted)
			{
				return new CustomerStatusAdvanced();
			}

			return this;
	    }
    }

    public class CustomerStatusAdvanced : CustomerStatus
    {
        public CustomerStatusAdvanced() : base(CustomerStatusName.Advanced, DateProviderFactory.DateProvider.Now().AddYears(1)) { }

        public override CustomerStatus NextStatus(Customer customer)
        {
	        var currentDate = DateProviderFactory.DateProvider.Now();

	        if (customer.Status.ValidTo.Value > currentDate)
	        {
		        return this;
	        }

	        var customerShouldBePromoted = HasBoughtTwoMoviesDuringTheLastMonth(customer, currentDate)
	                                       && HasSpentAtLeast100DuringTheLastYear(customer, currentDate);

	        if (customerShouldBePromoted)
	        {
		        return new CustomerStatusAdvanced();
	        }
	        else
	        {
		        return new CustomerStatusRegular();
	        }
		}
    }
}