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
    public interface ITutorialService
    {
        Task<List<TutorialCategory>> GetTutorialCategoriesAsync();
        Task<TutorialCategory> GetTutorialCategoryAsync(Guid id);
        Task<TutorialCategory> CreateTutorialCategoryAsync(CreateTutorialCategoryRequest value);
        Task<TutorialCategory> UpdateTutorialCategoryAsync(Guid id, UpdateTutorialCategoryRequest value);
        Task<bool> DeleteTutorialCategoryAsync(Guid id);
    }

    public class TutorialService : ITutorialService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TutorialService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        #region Tutorials
        #endregion

        #region Categories
        public async Task<List<TutorialCategory>> GetTutorialCategoriesAsync()
        {
            return await _dataContext.TutorialCategories.ToListAsync();
        }

        public async Task<TutorialCategory> GetTutorialCategoryAsync(Guid id)
        {
            return await _dataContext.TutorialCategories.SingleOrDefaultAsync(value => value.Id == id);
        }

        public async Task<TutorialCategory> CreateTutorialCategoryAsync(CreateTutorialCategoryRequest request)
        {
            var category = _mapper.Map<TutorialCategory>(request);

            await _dataContext.TutorialCategories.AddAsync(category);
            var created = await _dataContext.SaveChangesAsync();
            if (created > 0)
                return category;
            return null;
        }

        public async Task<TutorialCategory> UpdateTutorialCategoryAsync(Guid id, UpdateTutorialCategoryRequest request)
        {
            var category = await GetTutorialCategoryAsync(id);
            if (category == null)
                return null;

            category.Name = request.Name;

            _dataContext.TutorialCategories.Update(category);
            var updated = await _dataContext.SaveChangesAsync();
            if (updated > 0)
                return category;
            return null;
        }

        public async Task<bool> DeleteTutorialCategoryAsync(Guid id)
        {
            var category = await GetTutorialCategoryAsync(id);
            if (category == null)
                return false;

            _dataContext.TutorialCategories.Remove(category);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
        #endregion
    }
}
