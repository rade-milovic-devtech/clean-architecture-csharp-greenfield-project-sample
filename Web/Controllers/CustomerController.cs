using Application;
using Application.Customers.ViewCatalog;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	[Route("api/[controller]")]
	public class CustomerController : Controller
	{
		private readonly ViewCatalogService _viewCatalogService;

		[HttpGet("{customerId}")]
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
	}
}
