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

        public abstract CustomerStatus Refresh(Customer customer);
	}
}