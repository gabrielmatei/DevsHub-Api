using System;
using System.ComponentModel.DataAnnotations;

namespace DevsHub.Data
{
    public partial class Announcement
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public AnnouncementType Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public enum AnnouncementType
    {
        Info,
        Warning,
        Danger
    }
}
