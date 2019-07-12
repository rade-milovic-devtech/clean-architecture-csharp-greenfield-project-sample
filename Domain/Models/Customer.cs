namespace Domain.Models
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

