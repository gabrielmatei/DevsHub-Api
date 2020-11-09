using DevsHub.Data;
using DevsHub.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface IValueService
    {
        Task<List<Value>> GetValuesAsync();
        Task<Value> GetValueByIdAsync(Guid id);
        Task<bool> CreateValueAsync(Value value);
        Task<bool> UpdateValueAsync(Value value);
        Task<bool> DeleteValueAsync(Guid id);
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

        public async Task<Value> GetValueByIdAsync(Guid id)
        {
            return await _dataContext.Values.SingleOrDefaultAsync(value => value.Id == id);
        }

        public async Task<bool> CreateValueAsync(Value value)
        {
            await _dataContext.Values.AddAsync(value);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateValueAsync(Value value)
        {
            _dataContext.Values.Update(value);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteValueAsync(Guid id)
        {
            var value = await GetValueByIdAsync(id);
            if (value == null)
                return false;

            _dataContext.Values.Remove(value);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
