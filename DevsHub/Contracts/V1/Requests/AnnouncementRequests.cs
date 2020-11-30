using System;

namespace DevsHub.Contracts.V1.Requests
{
    public class CreateOrUpdateAnnouncementRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
