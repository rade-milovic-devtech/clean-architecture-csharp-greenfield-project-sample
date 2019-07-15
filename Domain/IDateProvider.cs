using System;

namespace Domain
{
	public interface IDateProvider
	{
		DateTime Now();
	}
}