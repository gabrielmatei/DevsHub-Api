using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using DevsHub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface IAnnouncementsService
    {
        Task<List<Announcement>> GetAnnouncementsAsync(bool showAll = false);
        Task<Announcement> CreateAnnouncementAsync(CreateOrUpdateAnnouncementRequest request);
        Task<Announcement> UpdateAnnouncementAsync(Guid id, CreateOrUpdateAnnouncementRequest request);
        Task<bool> DeleteAnnouncementAsync(Guid id);
    }

    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly IAnnouncementsRepository _announcements;
        private readonly IMapper _mapper;

        public AnnouncementsService(IAnnouncementsRepository announcements, IMapper mapper)
        {
            _announcements = announcements;
            _mapper = mapper;
        }

        public async Task<List<Announcement>> GetAnnouncementsAsync(bool showAll = false)
        {
            var currentDate = DateTime.UtcNow;
            return showAll
                ? await _announcements.GetListAsync()
                : await _announcements.GetListAsync(a => a.Start <= currentDate && currentDate <= a.End);
        }

        public async Task<Announcement> CreateAnnouncementAsync(CreateOrUpdateAnnouncementRequest request)
        {
            var announcement = _mapper.Map<Announcement>(request);
            return await _announcements.AddAsync(announcement);
        }

        public async Task<Announcement> UpdateAnnouncementAsync(Guid id, CreateOrUpdateAnnouncementRequest request)
        {
            var announcement = await _announcements.GetAsync(id);
            if (announcement == null)
                return null;

            announcement.Update(_mapper.Map<Announcement>(request));
            return await _announcements.UpdateAsync(announcement);
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            var announcement = await _announcements.GetAsync(id);
            if (announcement == null)
                return false;

            return await _announcements.DeleteAsync(announcement);
        }
    }
}
