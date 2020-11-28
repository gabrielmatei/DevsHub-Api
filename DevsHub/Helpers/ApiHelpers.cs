using Microsoft.AspNetCore.Http;
using System;

namespace DevsHub.Helpers
{
    public static class ApiHelper
    {
        public static string GetResourceUri(HttpRequest request, string resourcePath)
        {
            return $"{request.Scheme}://{request.Host.ToUriComponent()}/{resourcePath}";
        }

        public static string GetResourceUri(HttpRequest request, string resourcePath, Guid resourceId)
        {
            return $"{request.Scheme}://{request.Host.ToUriComponent()}/{resourcePath.Replace("{id}", resourceId.ToString())}";
        }
    }
}
