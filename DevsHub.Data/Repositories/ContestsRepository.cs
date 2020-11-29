using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Data.Repositories
{
    public interface IContestsRepository : IRepository<Contest>
    {
        Task<List<Contest>> GetContestsAsync();
        Task<Contest> GetContestAsync(Guid id);
    }

    public class ContestsRepository : Repository<Contest>, IContestsRepository
    {
        private DataContext _dataContext => this.Context as DataContext;

        public ContestsRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Contest>> GetContestsAsync()
        {
            return await _dataContext.Contests
                .Include(c => c.User).ThenInclude(u => u.Profile)
                .ToListAsync();
        }

        public async Task<Contest> GetContestAsync(Guid id)
        {
            return await _dataContext.Contests
                .Include(c => c.User).ThenInclude(u => u.Profile)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
