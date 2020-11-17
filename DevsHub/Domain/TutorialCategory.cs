using System;
using System.ComponentModel.DataAnnotations;

namespace DevsHub.Domain
{
    public class TutorialCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
