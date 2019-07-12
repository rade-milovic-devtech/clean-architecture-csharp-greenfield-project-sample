namespace Domain
{
	public class MoviePrice
	{
		public MoviePrice(decimal twoDays, decimal lifeLong)
		{
			TwoDays = twoDays;
			LifeLong = lifeLong;
		}

		public decimal TwoDays { get; }
		public decimal LifeLong { get; }
	}
}