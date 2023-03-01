using System;
using ApiRuleta.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRuleta.Repositories
{
	public class UserRepository : IUserRepository
	{
        protected RuletaDBContext _dBContext { get; set; }

        public UserRepository(RuletaDBContext dBContext) {
            _dBContext = dBContext;
		}

        public async Task Create(User user) {
            await _dBContext.Set<User>().AddAsync(user);
        }

        public async Task<User>? FindByName(string name) {
            var user = await _dBContext.Set<User>().Where(user => user.Name == name).FirstOrDefaultAsync();
            return user!;
        }

        public void Update(User user) {
            _dBContext.Set<User>().Update(user);
        }

        public async Task Save()
        {
            await _dBContext.SaveChangesAsync();
        }
    }
}

