using System.Collections.Generic;
using Application.Customers.ViewCatalog;
using Application.Ports;
using CSharpFunctionalExtensions;
using Domain.Models;
using Moq;
using Xunit;

namespace Application.Tests
{
	public class ViewCatalogTests
	{
		private readonly Mock<IGetCustomers> _getCustomersMock;
		private readonly Mock<IGetMovies> _getMoviesMock;
		private readonly ViewCatalogService _service;

		public ViewCatalogTests()
		{
			_getCustomersMock = new Mock<IGetCustomers>();
            _getMoviesMock = new Mock<IGetMovies>();

            _service = new ViewCatalogService(_getCustomersMock.Object, _getMoviesMock.Object);
		}

		[Fact]
		public void GetsMovieCatalog()
		{
			var customerId = "jkbfblf";

			_getCustomersMock.Setup(getCustomers => getCustomers.Get(customerId))
				.Returns(new Customer
				{
					Id = customerId,
					Name = "Djuro Djuric",
					Email = "djuro@email.com",
					Status = new CustomerStatusRegular()
				});
			_getMoviesMock.Setup(x => x.GetAll()).Returns(new List<Movie>
			{
				new Movie
				{
                    Id = "abc",
                    Price = new MoviePrice(12m, 12m),
                    Title = "Movie Title"
				}
			});
			var movieCatalogDto = _service.GetCatalog(customerId);

            Assert.True(movieCatalogDto.IsSuccess);
		}

		[Fact]
		public void FailsWhenCustomerIsNotFound()
		{
			var customerId = "jkbfblf";

			_getCustomersMock.Setup(getCustomers => getCustomers.Get(customerId))
				.Returns(Maybe<Customer>.None);
			
			var movieCatalogDto = _service.GetCatalog(customerId);

			Assert.True(movieCatalogDto.IsFailure);
		}
	}
}
