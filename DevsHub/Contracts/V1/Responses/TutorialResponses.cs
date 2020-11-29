using System;
using System.Collections.Generic;

namespace DevsHub.Contracts.V1.Responses
{
    #region Tutorials
    public class TutorialResponse
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public UserProfileResponse Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TutorialShortResponse
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public UserProfileResponse Author { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TutorialListResponse
    {
        public int Count { get; set; }
        public List<TutorialShortResponse> Tutorials { get; set; }
    }
    #endregion

    #region Categories
    public class TutorialCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TutorialCategoryListResponse
    {
        public int Count { get; set; }
        public List<TutorialCategoryResponse> TutorialCategories { get; set; }
    }
    #endregion
}
