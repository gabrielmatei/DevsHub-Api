using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using DevsHub.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface IUsersService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<User> UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(Guid id);
    }

    public class UsersService : IUsersService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UsersService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _dataContext.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await GetUserAsync(id);
            if (user == null)
                return null;

            user.Role = request.Role;
            user.UpdatedAt = DateTime.UtcNow;

            _dataContext.Users.Update(user);
            var updated = await _dataContext.SaveChangesAsync();
            if (updated > 0)
                return user;
            return null;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            // TODO inactive user

            var user = await GetUserAsync(id);
            if (user == null)
                return false;

            _dataContext.Users.Remove(user);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
