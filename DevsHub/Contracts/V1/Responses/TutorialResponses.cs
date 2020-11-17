using System;
using System.Collections.Generic;

namespace DevsHub.Contracts.V1.Responses
{
    #region Tutorials
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
