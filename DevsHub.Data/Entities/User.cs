using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevsHub.Data
{
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual UserProfile Profile { get; set; }
        public virtual ICollection<Contest> Contests { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
