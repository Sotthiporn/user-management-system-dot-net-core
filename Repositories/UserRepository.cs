using Microsoft.EntityFrameworkCore;
using user_management_dot_net_core.Database;
using user_management_dot_net_core.Models;
using user_management_dot_net_core.Repositories.Interfaces;

namespace user_management_dot_net_core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _dbContext.Users.ToListAsync();

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
