
using System.Collections.Generic;
using System.Linq;
using Application.Customers.ViewCatalog;
using Domain.Models;
using Xunit;

namespace Application.Tests
{
	public class MovieCatalogDtoMapperTests
	{
        [Fact]
		public void ShouldMapAllProperties()
		{
			var offer = new MovieOffer
			{
				MovieId = "abc",
				Price = new MoviePrice(12m, 14m),
				Title = "Movie Title"
			};

			var movieCatalogDto = MovieCatalogDtoMapper.MapToDto(new List<MovieOffer>{offer});


			Assert.Equal(1, movieCatalogDto.Movies.Count);
            Assert.Equal("abc", movieCatalogDto.Movies.First().Id);
            Assert.Equal("Movie Title", movieCatalogDto.Movies.First().Title);
            Assert.Equal(12m, movieCatalogDto.Movies.First().TwoDaysPrice);
            Assert.Equal(14m, movieCatalogDto.Movies.First().LifelongPrice);
		}
	}
}
