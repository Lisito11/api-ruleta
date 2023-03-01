using System;
using ApiRuleta.Helpers;
using ApiRuleta.Models;
using ApiRuleta.Repositories;
using Azure;

namespace ApiRuleta.Services
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
		{
            _repository = repository;
		}

        public async Task<ResponseBase<User>> Create(User user)
        {
            ResponseBase<User> response = new ResponseBase<User>();

            try
            {
                user.Name = user.Name!.ToUpper();
                await _repository.Create(user);
                await _repository.Save();
                response = new ResponseBase<User>(user);
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<ResponseBase<User>> GetByName(string name)
        {
            ResponseBase<User> response = new ResponseBase<User>();

            try
            {

                var userDB = await _repository.FindByName(name.ToUpper())!;

                if (userDB is null)
                {
                    response.Succeeded = false;
                    response.Error = "No hay usuario con ese nombre";
                    response.StatusCode = 404;
                    return response;
                }

                response.Data = userDB;

                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }

        public async Task<ResponseBase<bool>> Update(string name, User user)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();

            try
            {
                var userDB = await _repository.FindByName(name.ToUpper())!;

                if (userDB is null) {
                    response.Succeeded = false;
                    response.StatusCode = 404;
                    response.Error = "No se encuentra el usuario";
                    return response;
                }
                userDB.Amount = user.Amount;
                _repository.Update(userDB);
                await _repository.Save();
                return response;
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Error = ex.Message;
                return response;
            }
        }
    }
}

