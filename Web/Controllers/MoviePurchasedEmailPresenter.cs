using System;
using Application.Ports;
using CSharpFunctionalExtensions;
using Domain.Models;

namespace Web.Controllers
{
	public class MoviePurchasedEmailPresenter : IHandleMoviePurchased
	{
		public void Handle(Result<MovieOffer> movieOffer)
		{
			Console.WriteLine("Sending email for {0} movie", movieOffer.Value.Title);
		}
	}
}