using System;
using System.Collections.Generic;

namespace DevsHub.Contracts.V1.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserProfileResponse Profile { get; set; }
        public List<ContestShortResponse> Contests { get; set; }
        public List<TutorialShortResponse> Tutorials { get; set; }
    }

    public class UserShortResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserProfileResponse Profile { get; set; }
    }

    public class UserListResponse
    {
        public int Count { get; set; }
        public List<UserShortResponse> Users { get; set; }
    }

    public class UserProfileResponse
    {
        public string Name { get; set; }
        public int Rating { get; set; }
    }
}
