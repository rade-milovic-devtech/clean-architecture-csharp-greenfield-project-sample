using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Ports
{
	public interface IGetMovies
	{
		IList<Movie> GetAll();
		Maybe<Movie> Get(string movieId);
	}
}