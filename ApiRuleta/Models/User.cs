using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRuleta.Models
{
	[Table("User")]
	public class User
	{
		[Key]
		public string? Name { get; set; }

		public double Amount { get; set; }

	}
}

