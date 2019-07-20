using System.Collections.Generic;
using Application.Ports;
using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Infrastructure
{
	public class InMemoryCustomerGetter : IGetCustomers
	{
		private readonly Dictionary<string, Customer> customserStore;

		public InMemoryCustomerGetter()
		{
			customserStore = new Dictionary<string, Customer>
			{
				{
					"customer1", new Customer { Id = "customer1", Status = new CustomerStatusRegular(), Name = "Pera Peric", Email = "pera@gmail.com"}
				},
				{
					"customer2", new Customer { Id = "customer2", Status = new CustomerStatusAdvanced(), Name = "Maja Majic", Email = "maja@gmail.com"}
				}
			};	
		}

		public Maybe<Customer> Get(string customerId) => customserStore.TryGetValue(customerId, out var customer) ? customer : Maybe<Customer>.None;
	}
}
