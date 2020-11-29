using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Data.Repositories
{
    public interface ITutorialsRepository : IRepository<Tutorial>
    {
        Task<List<Tutorial>> GetTutorialsAsync();
        Task<Tutorial> GetTutorialAsync(Guid id);
    }

    public class TutorialsRepository : Repository<Tutorial>, ITutorialsRepository
    {
        private DataContext _dataContext => this.Context as DataContext;

        public TutorialsRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Tutorial>> GetTutorialsAsync()
        {
            return await _dataContext.Tutorials
                .Include(c => c.User).ThenInclude(u => u.Profile)
                .ToListAsync();
        }

        public async Task<Tutorial> GetTutorialAsync(Guid id)
        {
            return await _dataContext.Tutorials
                .Include(c => c.User).ThenInclude(u => u.Profile)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
