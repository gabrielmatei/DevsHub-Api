using System;

namespace DevsHub.Contracts.V1.Requests
{
    public class CreateOrUpdateContestRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
