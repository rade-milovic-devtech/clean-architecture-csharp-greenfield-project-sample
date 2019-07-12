using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
	public class Customer
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public CustomerStatus Status { get; set; }


	}

	public enum CustomerStatus
	{
        Regular,
        Advanced
	}
}

