using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using DevsHub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevsHub.Services
{
    public interface ITutorialService
    {
        #region Tutorials
        Task<List<Tutorial>> GetTutorialsAsync();
        Task<Tutorial> GetTutorialAsync(Guid id);
        Task<Tutorial> CreateTutorialAsync(Guid userId, CreateOrUpdateTutorialRequest request);
        Task<Tutorial> UpdateTutorialAsync(Guid id, Guid userId, CreateOrUpdateTutorialRequest request);
        Task<bool> DeleteTutorialAsync(Guid id, Guid userId);
        #endregion
        #region Categories
        Task<List<TutorialCategory>> GetTutorialCategoriesAsync();
        Task<TutorialCategory> GetTutorialCategoryAsync(Guid id);
        Task<TutorialCategory> CreateTutorialCategoryAsync(CreateOrUpdateTutorialCategoryRequest value);
        Task<TutorialCategory> UpdateTutorialCategoryAsync(Guid id, CreateOrUpdateTutorialCategoryRequest value);
        Task<bool> DeleteTutorialCategoryAsync(Guid id);
        #endregion
    }

    public class TutorialService : ITutorialService
    {
        private readonly ITutorialsRepository _tutorials;
        private readonly ITutorialCategoriesRepository _tutorialCategories;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public TutorialService(ITutorialsRepository tutorials, ITutorialCategoriesRepository tutorialCategories, IUsersService usersService, IMapper mapper)
        {
            _tutorials = tutorials;
            _tutorialCategories = tutorialCategories;
            _usersService = usersService;
            _mapper = mapper;
        }

        #region Tutorials
        public async Task<List<Tutorial>> GetTutorialsAsync()
        {
            return await _tutorials.GetTutorialsAsync();
        }

        public async Task<Tutorial> GetTutorialAsync(Guid id)
        {
            return await _tutorials.GetTutorialAsync(id);
        }

        public async Task<Tutorial> CreateTutorialAsync(Guid userId, CreateOrUpdateTutorialRequest request)
        {
            var tutorial = _mapper.Map<Tutorial>(request);
            tutorial.UserId = userId;
            tutorial.CreatedAt = DateTime.UtcNow;
            tutorial.UpdatedAt = DateTime.UtcNow;

            return await _tutorials.AddAsync(tutorial);
        }

        public async Task<Tutorial> UpdateTutorialAsync(Guid id, Guid userId, CreateOrUpdateTutorialRequest request)
        {
            var tutorial = await _tutorials.GetAsync(id);
            if (tutorial == null)
                return null;

            if (!await UserIsOwner(userId, tutorial.UserId))
                return null;

            tutorial.Update(_mapper.Map<Tutorial>(request));
            return await _tutorials.UpdateAsync(tutorial);
        }

        public async Task<bool> DeleteTutorialAsync(Guid id, Guid userId)
        {
            var tutorial = await _tutorials.GetAsync(id);
            if (tutorial == null)
                return false;

            if (!await UserIsOwner(userId, tutorial.UserId))
                return false;

            return await _tutorials.DeleteAsync(tutorial);
        }
        #endregion

        #region Categories
        public async Task<List<TutorialCategory>> GetTutorialCategoriesAsync()
        {
            return await _tutorialCategories.GetListAsync();
        }

        public async Task<TutorialCategory> GetTutorialCategoryAsync(Guid id)
        {
            return await _tutorialCategories.GetAsync(id);
        }

        public async Task<TutorialCategory> CreateTutorialCategoryAsync(CreateOrUpdateTutorialCategoryRequest request)
        {
            var tutorialCategory = _mapper.Map<TutorialCategory>(request);
            return await _tutorialCategories.AddAsync(tutorialCategory);
        }

        public async Task<TutorialCategory> UpdateTutorialCategoryAsync(Guid id, CreateOrUpdateTutorialCategoryRequest request)
        {
            var tutorialCategory = await _tutorialCategories.GetAsync(id);
            if (tutorialCategory == null)
                return null;

            tutorialCategory.Update(_mapper.Map<TutorialCategory>(request));
            return await _tutorialCategories.UpdateAsync(tutorialCategory);
        }

        public async Task<bool> DeleteTutorialCategoryAsync(Guid id)
        {
            var tutorialCategory = await _tutorialCategories.GetAsync(id);
            if (tutorialCategory == null)
                return false;

            return await _tutorialCategories.DeleteAsync(tutorialCategory);
        }
        #endregion

        private async Task<bool> UserIsOwner(Guid userId, Guid entityUserId)
        {
            var user = await _usersService.GetUserAsync(userId);
            if (user.Role == Role.Admin)
                return true;
            if (user.Id == entityUserId)
                return true;
            return false;
        }
    }
}
