using CSharpFunctionalExtensions;
using Domain.Models;

namespace Application.Ports
{
	public interface IHandleMoviePurchased
	{
		void Handle(Result<MovieOffer> movieOffer);
	}
}