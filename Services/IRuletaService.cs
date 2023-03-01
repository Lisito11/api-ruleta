using System;
using ApiRuleta.Helpers;
using ApiRuleta.Models;

namespace ApiRuleta.Services
{
	public interface IRuletaService
	{
		public Ruleta GetRandomNumberAndColor();

		public Task<ResponseBase<Object>> MakeBet(Bet betUser);

		public Task<double> LoseBet(double betAmount, string userName);

        public Task<double> WinBet(double betAmount, string userName, BetType betType);

		public bool IsEven(int number);

    }
}

