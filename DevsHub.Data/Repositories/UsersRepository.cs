using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Data.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid id);
    }

    public class UsersRepository : Repository<User>, IUsersRepository
    {
        private DataContext _dataContext => this.Context as DataContext;

        public UsersRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dataContext.Users
                .Include(u => u.Profile)
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _dataContext.Users
                .Include(u => u.Profile)
                .Include(u => u.Contests)
                .Include(u => u.Tutorials)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
