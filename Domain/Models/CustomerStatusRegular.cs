using System;

namespace Domain.Models
{
	public class CustomerStatusRegular : CustomerStatus
	{
		public CustomerStatusRegular() : base(CustomerStatusName.Regular) {}

		public override CustomerStatus Refresh(Customer customer)
		{
            var customerPromotionRule = new BoughtLeastMoviesDuringTheLastPeriod(2, TimeSpan.FromDays(30))
                .And(new SpentLeastAmountDuringTheLastPeriod(100m, TimeSpan.FromDays(365)));

            if (customerPromotionRule.IsSatisfiedBy(customer))
			{
				return new CustomerStatusAdvanced();
			}

			return this;
		}
	}
}