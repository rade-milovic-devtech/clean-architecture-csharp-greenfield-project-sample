using System;
using Domain.Models;

namespace Domain
{
	public class MoviePriceCalculatorFactory
	{
		public MoviePriceCalculator GetPriceCalculatorFor(Customer customer)
		{
			switch (customer.Status)
			{
				case CustomerStatusRegular _:
					return new Regular();
				case CustomerStatusAdvanced _:
					return new Advanced();
				default:
					throw new ArgumentException();
			}
		}
	}
}
