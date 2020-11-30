using System;
using System.Collections.Generic;

namespace DevsHub.Contracts.V1.Responses
{
    public class AnnouncementResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class AnnouncementListResponse
    {
        public int Count { get; set; }
        public List<AnnouncementResponse> Announcements { get; set; }
    }
}
