using System;
using System.Linq.Expressions;
using ApiRuleta.Models;

namespace ApiRuleta.Repositories
{
	public interface IUserRepository
	{
        Task<User>? FindByName(string name);
        Task Create(User user);
        void Update(User user);
        Task Save();
    }
}

