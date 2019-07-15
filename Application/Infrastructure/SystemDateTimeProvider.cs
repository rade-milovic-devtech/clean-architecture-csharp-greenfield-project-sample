using System;
using Domain;

namespace Application.Infrastructure
{
	public class SystemDateTimeProvider : IDateProvider
	{
		public DateTime Now() => DateTime.Now;
	}
}