using System.Collections.Generic;
using Domain.Models;

namespace Domain
{
	public interface IDefineCustomerPromotionRules
	{
		bool IsSatisfiedBy(Customer customer);
	}

	public static class CustomerPromotionRulesExtensions
	{
		public static IDefineCustomerPromotionRules And(this IDefineCustomerPromotionRules rule,
			IDefineCustomerPromotionRules andRule)
		{
			return new AndCustomerPromotionRules(new List<IDefineCustomerPromotionRules>
			{
				rule, andRule
			});
		}
	}
}