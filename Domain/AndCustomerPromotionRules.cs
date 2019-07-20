using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Domain
{
	public class AndCustomerPromotionRules : IDefineCustomerPromotionRules
	{
		private readonly IEnumerable<IDefineCustomerPromotionRules> _rules;

		public AndCustomerPromotionRules(IEnumerable<IDefineCustomerPromotionRules> rules)
		{
			_rules = rules;
		}

		public bool IsSatisfiedBy(Customer customer) => _rules.All(rule => rule.IsSatisfiedBy(customer));
	}
}