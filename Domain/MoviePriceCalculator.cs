using Domain.Models;

namespace Domain
{
	public abstract class MoviePriceCalculator
	{
		public abstract MoviePrice GetPrice(MoviePrice moviePrice);
	}

	public class Regular : MoviePriceCalculator
	{
		public override MoviePrice GetPrice(MoviePrice moviePrice)
		{
			return new MoviePrice(moviePrice.TwoDays, moviePrice.LifeLong);
		}
	}

	public class Advanced : MoviePriceCalculator
	{
		private const decimal Discount = 0.25m;

		public override MoviePrice GetPrice(MoviePrice moviePrice)
		{
            var twoDaysWithDiscount = moviePrice.TwoDays * (1 - Discount);
            var lifeLongWithDiscount = moviePrice.LifeLong * (1 - Discount);

			return new MoviePrice(twoDaysWithDiscount, lifeLongWithDiscount);
		}
	}


}