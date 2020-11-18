using System;
using System.Collections.Generic;

namespace DevsHub.Contracts.V1.Responses
{
    public class ContestShortResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid OrganizerId { get; set; }
        public UserProfileResponse Organizer { get; set; }
    }

    public class ContestShortListResponse
    {
        public int Count { get; set; }
        public List<ContestShortResponse> Contests { get; set; }
    }

    public class ContestResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid OrganizerId { get; set; }
        public UserProfileResponse Organizer { get; set; }
    }

    public class ContestListResponse
    {
        public int Count { get; set; }
        public List<ContestResponse> Contests { get; set; }
    }
}
