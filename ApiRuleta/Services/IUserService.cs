using System;
using ApiRuleta.Helpers;
using ApiRuleta.Models;

namespace ApiRuleta.Services
{
	public interface IUserService
	{
        Task<ResponseBase<User>> GetByName(string name);
        Task<ResponseBase<User>> Create(User user);
        Task<ResponseBase<bool>> Update(string name, User user);
    }
}

