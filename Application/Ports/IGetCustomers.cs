using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Ports
{
	public interface IGetCustomers
	{
		Maybe<Customer> Get(string customerId);
	}
}