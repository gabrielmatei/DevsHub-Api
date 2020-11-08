using DevsHub.Data;
using DevsHub.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface IValueService
    {
        Task<List<Value>> GetValuesAsync();
    }

    public class ValueService : IValueService
    {
        private readonly DataContext _dataContext;

        public ValueService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Value>> GetValuesAsync()
        {
            return await _dataContext.Values.ToListAsync();
        }
    }
}
