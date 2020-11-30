using DevsHub.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace DevsHub.SwaggerExamples.V1.Responses
{
    public class AuthenticationResponseExample : IExamplesProvider<AuthenticationResponse>
    {
        public AuthenticationResponse GetExamples() => new AuthenticationResponse
        {
            Token = "xxxxxxxxxxxxxxxx"
        };
    }
}
