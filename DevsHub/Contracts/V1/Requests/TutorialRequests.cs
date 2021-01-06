using System;
using System.Collections.Generic;

namespace DevsHub.Contracts.V1.Requests
{
    #region Tutorials
    public class CreateOrUpdateTutorialRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Guid> Categories { get; set; }
    }
    #endregion

    #region Categories
    public class CreateOrUpdateTutorialCategoryRequest
    {
        public string Name { get; set; }
    }
    #endregion
}
