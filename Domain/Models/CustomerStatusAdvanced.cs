using System;

namespace Domain.Models
{
	public class CustomerStatusAdvanced : CustomerStatus
	{
		public CustomerStatusAdvanced() : base(CustomerStatusName.Advanced, DateProviderFactory.DateProvider.Now().AddYears(1)) { }

		public override CustomerStatus Refresh(Customer customer)
		{
			var currentDate = DateProviderFactory.DateProvider.Now();

			if (ValidTo.Value >= currentDate)
			{
				return this;
			}

            var customerPromotionRule = new BoughtLeastMoviesDuringTheLastPeriod(2, TimeSpan.FromDays(30))
                .And(new SpentLeastAmountDuringTheLastPeriod(100m, TimeSpan.FromDays(365)));

			if (customerPromotionRule.IsSatisfiedBy(customer))
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