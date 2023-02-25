using System;
using ApiRuleta.Helpers;

namespace ApiRuleta.Models
{
	public class Ruleta
	{
		public int RandomNumber { get; set; }

		public string? Color { get; set; }

        public NumberType? NumberType { get; set; }

    }
}

