using Application.Ports;
using CSharpFunctionalExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class MoviePurchasedWebApiPresenter : IHandleMoviePurchased
	{
		public IActionResult Result { get; private set; }

		public void Handle(Result<MovieOffer> movieOffer)
		{
			Result = movieOffer.IsSuccess ? (IActionResult)new OkResult() : new NotFoundObjectResult(movieOffer.Error);
		}
	}
}