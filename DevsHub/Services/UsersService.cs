using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using DevsHub.Data.Repositories;
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
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _usersRepository.GetUsersAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _usersRepository.GetUserAsync(id);
        }

        public async Task<User> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await _usersRepository.GetAsync(id);
            if (user == null)
                return null;

            user.Update(_mapper.Map<User>(request));
            return await _usersRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _usersRepository.GetAsync(id);
            if (user == null)
                return false;

            return await _usersRepository.DeleteAsync(user);
        }
    }
}
