using DevsHub.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace DevsHub.SwaggerExamples.V1.Requests
{
    public class LoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples() => new LoginRequest
        {
            Email = "admin@test.com",
            Password = "123456"
        };
    }
}
