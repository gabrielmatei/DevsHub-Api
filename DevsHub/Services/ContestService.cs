using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using DevsHub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface IContestService
    {
        Task<List<Contest>> GetContestsAsync();
        Task<Contest> GetContestAsync(Guid id);
        Task<Contest> CreateContestAsync(Guid userId, CreateOrUpdateContestRequest request);
        Task<Contest> UpdateContestAsync(Guid id, Guid userId, CreateOrUpdateContestRequest request);
        Task<bool> DeleteContestAsync(Guid id, Guid userId);
    }

    public class ContestService : IContestService
    {
        private readonly IContestsRepository _contests;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public ContestService(IContestsRepository contests, IUsersService usersService, IMapper mapper)
        {
            _contests = contests;
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<List<Contest>> GetContestsAsync()
        {
            return await _contests.GetContestsAsync();
        }

        public async Task<Contest> GetContestAsync(Guid id)
        {
            return await _contests.GetContestAsync(id);
        }

        public async Task<Contest> CreateContestAsync(Guid userId, CreateOrUpdateContestRequest request)
        {
            var contest = _mapper.Map<Contest>(request);
            contest.UserId = userId;

            return await _contests.AddAsync(contest);
        }

        public async Task<Contest> UpdateContestAsync(Guid id, Guid userId, CreateOrUpdateContestRequest request)
        {
            var contest = await _contests.GetAsync(id);
            if (contest == null)
                return null;

            if (!await UserIsOwner(userId, contest.UserId))
                return null;

            contest.Update(_mapper.Map<Contest>(request));
            return await _contests.UpdateAsync(contest);
        }

        public async Task<bool> DeleteContestAsync(Guid id, Guid userId)
        {
            var contest = await _contests.GetAsync(id);
            if (contest == null)
                return false;

            if (!await UserIsOwner(userId, contest.UserId))
                return false;

            return await _contests.DeleteAsync(contest);
        }

        private async Task<bool> UserIsOwner(Guid userId, Guid entityUserId)
        {
            var user = await _usersService.GetUserAsync(userId);
            if (user.Role == Role.Admin)
                return true;
            if (user.Role == Role.Organizer && user.Id == entityUserId)
                return true;
            return false;
        }
    }
}
