using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Domain.Entities;

namespace Domain
{
	public class MoviePriceCalculatorFactory
	{
		public MoviePriceCalculator GetPriceCalculatorFor(Customer customer)
		{
			switch (customer.Status)
			{
				case CustomerStatus.Regular:
					return new Regular();
				case CustomerStatus.Advanced:
					return new Advanced();
				default:
					throw new InvalidEnumArgumentException();
			}
		}
	}
}
