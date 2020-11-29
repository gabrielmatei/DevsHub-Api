using System;

namespace DevsHub.Data
{
    public partial class User
    {
        public void Update(User entity)
        {
            this.Role = entity.Role;
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
