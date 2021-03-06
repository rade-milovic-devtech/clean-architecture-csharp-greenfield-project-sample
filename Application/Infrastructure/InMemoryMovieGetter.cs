using System.Collections.Generic;
using System.Linq;
using Application.Ports;
using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Infrastructure
{
	public class InMemoryMovieGetter : IGetMovies
	{
		private readonly Dictionary<string, Movie> movies;

		public InMemoryMovieGetter()
		{
			this.movies = new Dictionary<string, Movie>
			{
				{
					"movie1", new Movie { Id = "movie1", Title = "Catch me if you can", Price = new MoviePrice(10.5m, 105m)}
				},
				{
					"movie2", new Movie { Id = "movie2", Title = "Fight Club", Price = new MoviePrice(12.5m, 125m) }
				}
			}; ;
		}

		public IList<Movie> GetAll() => movies.Values.ToList();
		public Maybe<Movie> Get(string movieId) => movies.TryGetValue(movieId, out var movie) ? movie : Maybe<Movie>.None;
	}
}
