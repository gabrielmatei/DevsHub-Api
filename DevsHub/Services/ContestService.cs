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
        private readonly DataContext _dataContext;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public ContestService(DataContext dataContext, IUsersService usersService, IMapper mapper)
        {
            _dataContext = dataContext;
            _usersService = usersService;
            _mapper = mapper;
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

        public async Task<Contest> CreateContestAsync(Guid userId, CreateOrUpdateContestRequest request)
        {
            var contest = _mapper.Map<Contest>(request);
            contest.UserId = userId;

            await _dataContext.Contests.AddAsync(contest);
            var created = await _dataContext.SaveChangesAsync();
            if (created > 0)
                return contest;
            return null;
        }

        public async Task<Contest> UpdateContestAsync(Guid id, Guid userId, CreateOrUpdateContestRequest request)
        {
            var contest = await GetContestAsync(id);
            if (contest == null)
                return null;

            bool canUpdate = false;
            var currentUser = await _usersService.GetUserAsync(userId);
            if (currentUser.Role == Role.Admin)
                canUpdate = true;
            if (currentUser.Role == Role.Organizer && currentUser.Id == contest.UserId)
                canUpdate = true;

            if (!canUpdate)
                return null;

            contest.Name = request.Name;
            contest.Description = request.Description;
            contest.Start = request.Start;
            contest.End = request.End;

            _dataContext.Contests.Update(contest);
            var updated = await _dataContext.SaveChangesAsync();
            if (updated > 0)
                return contest;
            return null;
        }

        public async Task<bool> DeleteContestAsync(Guid id, Guid userId)
        {
            var contest = await GetContestAsync(id);
            if (contest == null)
                return false;

            bool canDelete = false;
            var currentUser = await _usersService.GetUserAsync(userId);
            if (currentUser.Role == Role.Admin)
                canDelete = true;
            if (currentUser.Role == Role.Organizer && currentUser.Id == contest.UserId)
                canDelete = true;

            if (!canDelete)
                return false;

            _dataContext.Contests.Remove(contest);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
