using System;

namespace DevsHub.Contracts.V1.Responses
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
    }

    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserProfileResponse Profile { get; set; }
    }
}
