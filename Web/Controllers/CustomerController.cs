using System.Collections.Generic;
using Application;
using Application.Customers;
using Application.Customers.BuyMovie;
using Application.Customers.ViewCatalog;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	[Route("api/[controller]")]
	public class CustomerController : Controller
	{
		private readonly ViewCatalogService _viewCatalogService;
		private readonly BuyMovieService _buyMovieService;

		public CustomerController(ViewCatalogService viewCatalogService, BuyMovieService buyMovieService)
		{
			_viewCatalogService = viewCatalogService;
			_buyMovieService = buyMovieService;
		}

		[HttpGet("{customerId}/movies")]
		public IActionResult GetMovieCatalog(string customerId)
		{
			var result = _viewCatalogService.GetCatalog(customerId);

			if (result.IsSuccess)
			{
				return Ok(result.Value);
			}
			else if (result.Error.Equals(Errors.CustomerNotFound))
			{
				return NotFound(result.Error);
			}

			return BadRequest();
		}

		[HttpPost("{customerId}/movies")]
		public IActionResult BuyMovie(string customerId, [FromBody]BuyMovieRequest buyMovieRequest)
		{
			var webApiPresenter = new MoviePurchasedWebApiPresenter();
			var emailPresenter = new MoviePurchasedEmailPresenter();

			_buyMovieService.BuyMovie(new BuyMovieDto
			{
				CustomerId = customerId,
				MovieId = buyMovieRequest.MovieId,
				MovieLicenseType = buyMovieRequest.LicenseType
			}, new List<IHandleMoviePurchased>{ webApiPresenter, emailPresenter });

			return webApiPresenter.Result;
		}

	}

	public class BuyMovieRequest
	{
		public string MovieId { get; set; }
		public MovieLicenseType LicenseType { get; set; }

	}
}
