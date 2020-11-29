using System;

namespace DevsHub.Data
{
    public partial class Tutorial
    {
        public void Update(Tutorial entity)
        {
            this.Title = entity.Title;
            this.Content = entity.Content;
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
