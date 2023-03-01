using System;
using ApiRuleta.Helpers;

namespace ApiRuleta.Models
{
	public class Bet
    {
        public int Number { get; set; }

        public string? Color { get; set; }

        public NumberType? NumberType { get; set; }

        public double Amount { get; set; }

        public BetType betType { get; set; }

        public string? UserName { get; set; }

        public Ruleta? Ruleta { get; set; }
    }

   
}

