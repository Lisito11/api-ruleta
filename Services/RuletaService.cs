using System;
using ApiRuleta.Helpers;
using ApiRuleta.Models;
using ApiRuleta.Repositories;

namespace ApiRuleta.Services
{
	public class RuletaService : IRuletaService
	{
        private readonly IUserRepository _repository;

        public RuletaService(IUserRepository repository) {
            _repository = repository;
        }

        public Ruleta GetRandomNumberAndColor() {

            // Get random number between 0 and 36
            Random r = new Random();
            int randomNumber = r.Next(37);

            // Get random color between red and black
            int randomIndex = r.Next(2);
            List<string> colors = new List<string>() { "Rojo", "Negro" };
            string randomColor = colors[randomIndex];

            //Get numberType
            NumberType numberType = IsEven(randomNumber) ? NumberType.Even : NumberType.Odd;


            Ruleta ruleta = new Ruleta()
            {
                RandomNumber = randomNumber,
                Color = randomColor,
                NumberType = numberType
            };

            return ruleta;
        }

        

        public async Task<ResponseBase<Object>> MakeBet(Bet betUser) {

            ResponseBase<Object> response = new ResponseBase<Object>();

            var betAmount = betUser.Amount;
            var betType = betUser.betType;
            var user = betUser.UserName!;
            var ruleta = betUser.Ruleta!;
            double result = 0;

            switch (betType) {

                case BetType.Color:

                    if (betUser.Color == ruleta.Color) {
                        result = await WinBet(betAmount, user, betType);
                        response.Data = new { Amount = result, Status = true };
                        return response;
                    }

                    break;

                case BetType.ColorEven:

                    if (betUser.Color == ruleta.Color && ruleta.NumberType == NumberType.Even) {
                        result = await WinBet(betAmount, user, betType);
                        response.Data = new { Amount = result, Status = true };
                        return response;
                    }

                    break;

                case BetType.ColorOdd:

                    if (betUser.Color == ruleta.Color && ruleta.NumberType == NumberType.Odd) {
                        result = await WinBet(betAmount, user, betType);
                        response.Data = new { Amount = result, Status = true };
                        return response;
                    }

                    break;

                case BetType.NumberColor:

                    if (betUser.Color == ruleta.Color && betUser.Number == ruleta.RandomNumber) {
                        result = await WinBet(betAmount, user, betType);
                        response.Data = new { Amount = result, Status = true };
                        return response;
                    }

                    break;

            }

            result = await LoseBet(betAmount, user);
            response.Data = new { Amount = result, Status = false };
            return response;

        }


        public async Task<double> LoseBet(double betAmount, string userName) {

            var userDB = await _repository.FindByName(userName.ToUpper())!;

            if (userDB is not null) {
                userDB.Amount -= betAmount;
                _repository.Update(userDB);
                await _repository.Save();
            }

            return 0;
        }

        public async Task<double> WinBet(double betAmount, string userName, BetType betType) {

            var userDB = await _repository.FindByName(userName.ToUpper())!;
            double winAmount = 0;

            if (betType == BetType.Color) {
                winAmount = betAmount / 2;
            }

            if (betType == BetType.ColorEven || betType == BetType.ColorOdd) {
                winAmount = betAmount;
            }

            if (betType == BetType.NumberColor) {
                winAmount = betAmount * 3;
            }

            if (userDB is not null)
            {
                userDB.Amount += winAmount;
                _repository.Update(userDB);
                await _repository.Save();
            }


            return winAmount;


        }

        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}

