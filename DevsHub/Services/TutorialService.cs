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
        Task<List<TutorialCategory>> GetTutorialCategoriesAsync();
        Task<TutorialCategory> GetTutorialCategoryAsync(Guid id);
        Task<TutorialCategory> CreateTutorialCategoryAsync(CreateOrUpdateTutorialCategoryRequest value);
        Task<TutorialCategory> UpdateTutorialCategoryAsync(Guid id, CreateOrUpdateTutorialCategoryRequest value);
        Task<bool> DeleteTutorialCategoryAsync(Guid id);
    }

    public class TutorialService : ITutorialService
    {
        private readonly ITutorialCategoriesRepository _tutorialCategories;
        private readonly IMapper _mapper;

        public TutorialService(ITutorialCategoriesRepository tutorialCategories, IMapper mapper)
        {
            _tutorialCategories = tutorialCategories;
            _mapper = mapper;
        }

        #region Tutorials
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
    }
}
