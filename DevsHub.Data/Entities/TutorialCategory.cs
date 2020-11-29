using System;
using System.ComponentModel.DataAnnotations;

namespace DevsHub.Data
{
    public partial class TutorialCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
