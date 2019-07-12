using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
	public class Movie
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public MoviePrice Price { get; set; }
	}
}